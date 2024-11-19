using Health.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Health.Controllers
{
    public class UserController : Controller
    {
        private readonly HealthContext _healthContext;

        public UserController()
        {
            _healthContext = new HealthContext();
        }

        public IActionResult GetTicket()
        {

            ViewData["SpecId"] = new SelectList(_healthContext.Specializations, "SpecId", "SpecName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetTicket([Bind("TicketId,FName,SName,FatherName,Birthday,PassNum,PassSeries,AppDate,SpecId")] Ticket ticket)
        {
            ticket.IssueDate = DateTime.Now;
            ticket.ClientCardId = null;
            bool isDateValid = ticket.AppDate > DateTime.Now;
            
            if (ModelState.IsValid && isDateValid)
            {
                _healthContext.Tickets.Add(ticket);
                await _healthContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["SpecId"] = new SelectList(_healthContext.Specializations, "SpecId", "SpecName", ticket.SpecId);
            if (!isDateValid) ViewData["WrongDate"] = "Неверная дата!";
            return View(ticket);
        }
    }
}
