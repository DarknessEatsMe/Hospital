using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health.Controllers
{
    public class HomeController : Controller
    {
        private readonly HealthContext _healthContext;

        public HomeController()
        {
            _healthContext = new HealthContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        async public Task<IActionResult> GetAllTickets()
        {
            var ticketsList = await _healthContext.Tickets.ToListAsync();
            var spec = await _healthContext.Specializations.ToListAsync();

            var tickets = (from t in ticketsList
                           join s in spec on t.SpecId equals s.SpecId
                           where t.ClientCardId == null && t.AppDate > DateTime.Now.Date
                           orderby t.AppDate
                           select new
                           {
                               t.FName,
                               t.SName,
                               t.FatherName,
                               t.Birthday,
                               t.IssueDate,
                               t.AppDate,
                               s.SpecName
                           }).ToList<dynamic>();

            return View(tickets);
        }

        [HttpPost]
        async public Task<IActionResult> GetAllTickets(string FName, string SName)
        {
            var ticketsList = await _healthContext.Tickets.ToListAsync();
            var spec = await _healthContext.Specializations.ToListAsync();

            var tickets = (from t in ticketsList
                          join s in spec on t.SpecId equals s.SpecId
                          where t.ClientCardId == null && t.AppDate > DateTime.Now.Date 
                          && t.FName.Equals(FName)
                          && t.SName.Equals(SName)
                          orderby t.AppDate
                          select new
                          {
                              t.FName,
                              t.SName,
                              t.FatherName,
                              t.Birthday,
                              t.IssueDate,
                              t.AppDate,
                              s.SpecName
                          }).ToList<dynamic>();

            ViewData["FName"] = FName;
            ViewData["SName"] = SName;
            return View(tickets);
        }
    }
}
