using Microsoft.AspNetCore.Mvc;
using Health.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Health.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.Versioning;
using System.Net.Sockets;
using System.Configuration;

namespace Health.Controllers
{
    public class DoctorController : Controller
    {
        private readonly HealthContext _healthContext;

        public DoctorController()
        {
            _healthContext = new HealthContext();
        }

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DocPage(int? id)
        {
            int cookieId = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
            if (cookieId == id && id != null) 
            {
                Doctor? doc = await _healthContext.Doctors.FindAsync(id);
                if (doc != null)
                {
                    var ticketsList = await _healthContext.Tickets.ToListAsync();
                    var docTickets = from t in ticketsList
                                     where t.SpecId == doc.SpecId && t.ClientCardId == null && t.AppDate > DateTime.Now.Date 
                                     && t.AppDate < DateTime.Now.AddDays(1).Date
                                     orderby t.AppDate
                                     select t;

                    PersonInfo? personInfo = await _healthContext.PersonInfos.FindAsync(doc.PersonInfoId);

                    DocPageModels docPageModels = new DocPageModels() { Doc = doc, Tickets = docTickets, Person = personInfo};
                    return View(docPageModels);
                }
                return NotFound();
            }
            return NotFound();
        }

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Appointment(int id)
        {
            ViewData["DocId"] = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value);
            Ticket? ticket = await _healthContext.Tickets.FindAsync(id);
            AppointmentModels appointmentModels = new AppointmentModels();

            var docServices = await _healthContext.DoctorServices.ToListAsync();
            var services = await _healthContext.Services.ToListAsync();

            var curDocServices = from ds in docServices
                                 where ds.DocId == Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value)
                                 select ds;

            var servNames = from s in services
                            join cds in curDocServices on s.ServId equals cds.ServId
                            select new
                            {
                                Names = s.ServName,
                                Id = s.ServId
                            };
            
            ViewData["Services"] = new SelectList(servNames, "Id", "Names");
            if (ticket != null && ticket.ClientCardId == null)
            {
                PersonInfo? info = await _healthContext.PersonInfos.FirstOrDefaultAsync(i => i.PassNum.Equals(ticket.PassNum) && i.PassSeries.Equals(ticket.PassSeries));
                if (info != null)
                {
                    ClientsCard? card = await _healthContext.ClientsCards.FirstOrDefaultAsync(c => c.PersonInfoId == info.PersonInfoId);
                    if (card != null)
                    {
                        var appsList = await _healthContext.Appointments.ToListAsync();

                        var appData = (from a in appsList
                                      join ds in _healthContext.DoctorServices.ToList() on a.DocServId equals ds.DocServId
                                      join d in _healthContext.Diagnoses.ToList() on a.DiagId equals d.DiagId
                                      join doc in _healthContext.Doctors.ToList() on ds.DocId equals doc.DocId
                                      join p in _healthContext.PersonInfos.ToList() on doc.PersonInfoId equals p.PersonInfoId
                                      where a.ClientCardId == card.ClientCardId
                                      select new
                                      {
                                          a.AppId,
                                          a.AppDate,
                                          d.DiagName,
                                          DocName = p.FName + " " + p.SName + " " + p.FatherName,
                                          a.Price
                                      }).ToList<dynamic>();

                        
                        if (appData.Count() != 0)
                        {
                            appointmentModels = new AppointmentModels() { Ticket = ticket, Card = card, Info = info, Appointments = appData };
                        }
                        else
                        {
                            appointmentModels = new AppointmentModels() { Ticket = ticket, Card = card, Info = info };
                        }
                        return View(appointmentModels);
                    }
                    appointmentModels = new AppointmentModels() { Ticket = ticket, Info = info };
                    return View(appointmentModels);
                }
                appointmentModels = new AppointmentModels() { Ticket = ticket };
                return View(appointmentModels);
            }
            return RedirectToAction("DocPage", new {id = Convert.ToInt32(User.FindFirst(ClaimTypes.Name)?.Value)});
        }

        [HttpPost]
        [Authorize]
        async public Task<IActionResult> Appointment(int CardId, int DiagId, int ServId, string Course, int TicketId, int Price)
        {
            Appointment appointment = new Appointment();
            appointment.ClientCardId = CardId;
            appointment.DiagId = DiagId;

            DoctorService? DocServ = await _healthContext.DoctorServices.FirstOrDefaultAsync(ds => 
                ds.DocId == Convert.ToInt32(User.FindFirst(ClaimTypes.Name).Value)
                && ds.ServId == ServId
            );
            
            appointment.DocServId = DocServ.DocServId;
            appointment.Course = Course;
            appointment.AppDate = DateOnly.FromDateTime(DateTime.Now.Date);
            appointment.Price = Price;

            _healthContext.Appointments.Add(appointment);
            Ticket? ticket = await _healthContext.Tickets.FindAsync(TicketId);
            if (ticket != null)
            {
                ticket.ClientCardId = CardId;
                _healthContext.Update(ticket);
            }
            await _healthContext.SaveChangesAsync();

            return RedirectToAction("DocPage", new { id = User.FindFirst(ClaimTypes.Name)?.Value });
        }


        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Createcard(int id)
        {
            Ticket? ticket = await _healthContext.Tickets.FindAsync(id);
            IEnumerable<Sex> sex = new Sex[]
            {
                new Sex("Мужской", false),
                new Sex("Женский", true)
            };

            ViewData["Sex"] = new SelectList(sex, "Value", "Name");
            ViewData["TicketId"] = id;
            if (ticket != null)
            {
                PersonInfo info = new PersonInfo();
                info.FName = ticket.FName;
                info.SName = ticket.SName;
                info.FatherName = ticket.FatherName;
                info.PassNum = ticket.PassNum;
                info.PassSeries = ticket.PassSeries;
                info.Birthday = ticket.Birthday;
                info.Sex = false;
                PersonInfo? exInfo = await _healthContext.PersonInfos.FirstOrDefaultAsync(e => 
                    e.PassNum.Equals(ticket.PassNum) && e.PassSeries.Equals(ticket.PassSeries)
                );
                if (exInfo != null)
                {
                    info.Adress = exInfo.Adress;
                    info.Discount = exInfo.Discount;
                }
                return View(info);
            }
            return View();
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<IActionResult> Createcard([Bind("PersonInfoId,FName,SName,FatherName,Birthday,PassNum,PassSeries,Adress,Sex,Discount")] 
            PersonInfo info, int TicketId, string CardNum
        )
        {
            ClientsCard? cardnum = await _healthContext.ClientsCards.FirstOrDefaultAsync(c => c.CardNum.Equals(CardNum));
            if (ModelState.IsValid && cardnum == null)
            {
                ClientsCard card = new ClientsCard();
                PersonInfo? exInfo = await _healthContext.PersonInfos.FirstOrDefaultAsync(e => 
                    e.PassNum.Equals(info.PassNum) && e.PassSeries.Equals(info.PassSeries)
                );

                if (exInfo != null) {
                    card.PersonInfoId = exInfo.PersonInfoId;
                } 
                else
                {
                    _healthContext.PersonInfos.Add(info);
                    await _healthContext.SaveChangesAsync();

                    card.PersonInfoId = info.PersonInfoId;
                }
                card.CardNum = CardNum;
                card.CreationDate = DateOnly.FromDateTime(DateTime.Now);

                _healthContext.ClientsCards.Add(card);
                await _healthContext.SaveChangesAsync();

                Ticket? ticket = await _healthContext.Tickets.FindAsync(TicketId);
                if (ticket != null)
                {
                    return RedirectToAction("Appointment", "Doctor", new { id = TicketId});
                }
                return RedirectToAction("DocPage", "Doctor", new { id = User.FindFirst(ClaimTypes.Name)?.Value });
            }

            IEnumerable<Sex> sex = new Sex[]
            {
                new Sex("Мужской", false),
                new Sex("Женский", true)
            };

            ViewData["Sex"] = new SelectList(sex, "Value", "Name");
            ViewData["TicketId"] = TicketId;
            ViewData["CardExist"] = "Карта с таким номером уже существует";
            return View(info);
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetDiag(string mkbCode)
        {
            Diagnosis? diagnosis = _healthContext.Diagnoses.FirstOrDefault(d => d.MkbCode.Equals(mkbCode));
            if (diagnosis != null)
            {
                return Json(diagnosis);
            }
            Console.WriteLine("NULL");
            return Json(null);
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetPrice(int servId)
        {
            DoctorService? DocServ = _healthContext.DoctorServices.FirstOrDefault(ds =>
                 ds.DocId == Convert.ToInt32(User.FindFirst(ClaimTypes.Name).Value) && ds.ServId == servId
            );

            if (DocServ != null)
            {
                return Json(DocServ.Price);
            }
            return Json(null);
        }

        [HttpGet]
        [Authorize]
        async public Task<IActionResult> Money()
        {
            var apps = await _healthContext.Appointments.ToListAsync();
            var docServices = await _healthContext.DoctorServices.ToListAsync();
            var services = await _healthContext.Services.ToListAsync();

            var money = from a in apps
                        group a by a.DocServId into g
                        select new
                        {
                            Id = g.Key,
                            Money = g.Sum(f => f.Price)
                        };

            var list = from m in money
                       join ds in docServices on m.Id equals ds.DocServId
                       join s in services on ds.ServId equals s.ServId
                       select new
                       {
                           Name = s.ServName,
                           Money = m.Money
                       };
            List<Money> listM = new List<Money>();

            foreach ( var item in list )
            {
                listM.Add(new Money(item.Name, item.Money));
            }

            return View(listM);

        }

        [HttpGet]
        [Authorize]
        async public Task<IActionResult> WorkLoad()
        {
            var tickets = await _healthContext.Tickets.ToListAsync();
            var specs = await _healthContext.Specializations.ToListAsync();

            var gTickets = from t in tickets
                        group t by t.SpecId into g
                        select new
                        {
                            Id = g.Key,
                            Count = g.Count()
                        };

            var list = (from gt in gTickets
                       join s in specs on gt.Id equals s.SpecId
                       select new
                       {
                           Name = s.SpecName,
                           gt.Count
                       }).ToList<dynamic>();

            return View(list);

        }

        [HttpPost]
        [Authorize]
        async public Task<IActionResult> WorkLoad(DateTime dateFrom, DateTime dateTo)
        {
            var tickets = await _healthContext.Tickets.ToListAsync();
            var specs = await _healthContext.Specializations.ToListAsync();

            var gTickets = from t in tickets
                           where t.AppDate > dateFrom && t.AppDate < dateTo
                           group t by t.SpecId into g
                           select new
                           {
                               Id = g.Key,
                               Count = g.Count()
                           };

            var list = (from gt in gTickets
                        join s in specs on gt.Id equals s.SpecId
                        select new
                        {
                            Name = s.SpecName,
                            gt.Count
                        }).ToList<dynamic>();
            ViewData["From"] = dateFrom.ToString("s");
            ViewData["To"] = dateTo.ToString("s");
            return View(list);

        }

        [HttpGet]
        async public Task<IActionResult> GetDiagnoses()
        {
            var diagsList = await _healthContext.Diagnoses.ToListAsync();

            var diags = from d in diagsList
                          select d;

            return View(diags);
        }

        [HttpPost]
        async public Task<IActionResult> GetDiagnoses(string Name)
        {
            var diagsList = await _healthContext.Diagnoses.ToListAsync();

            var diags = from d in diagsList
                        where d.DiagName.Equals(Name)
                        select d;
            ViewData["Name"] = Name;
            Console.WriteLine(Name);
            return View(diags);
        }
    }
}
