using Health.Models;
using Health.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Health.Controllers
{
    public class Study : Controller
    {

        private readonly HealthContext _healthContext;

        public Study()
        {
            _healthContext = new HealthContext();
        }

        public IActionResult MainView()
        {
            return View();
        }

        public IActionResult DocCreation()
        {
            IEnumerable<Sex> sex = new Sex[]
            {
                new Sex("Мужской", false),
                new Sex("Женский", true)
            };
            ViewData["SpecId"] = new SelectList(_healthContext.Specializations, "SpecId", "SpecName");
            ViewData["CatId"] = new SelectList(_healthContext.Categories, "CatId", "CatName");
            ViewData["Sex"] = new SelectList(sex, "Value", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DocCreation(PersonInfo info, Doctor doc)
        {

            
            await _healthContext.PersonInfos.AddAsync(info);
            await _healthContext.SaveChangesAsync();

            doc.PersonInfoId = info.PersonInfoId;
            await _healthContext.Doctors.AddAsync(doc);
            await _healthContext.SaveChangesAsync();

            return RedirectToAction("DocCreation");
        }

        [HttpPost]
        public async Task<IActionResult> MainView(Diagnosis diag, Specialization spec)
        {
            
            await _healthContext.Diagnoses.AddAsync(diag);
            await _healthContext.Specializations.AddAsync(spec);
            await _healthContext.SaveChangesAsync();
            return RedirectToAction("Diagnoses");
        }

        [HttpGet]
        public IActionResult Diagnoses()
        {
            return View();
        }
    }
}
