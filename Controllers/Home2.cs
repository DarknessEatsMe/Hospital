using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health;
using Health.Models;

namespace Health.Controllers
{
    public class Home2 : Controller
    {
        private readonly HealthContext _healthContext;

        public Home2() 
        {
            _healthContext = new HealthContext();
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            var docs = await _healthContext.Doctors.ToListAsync();
            var infos = await _healthContext.PersonInfos.ToListAsync();
            var list = from i in infos 
                       join d in docs on i.PersonInfoId equals d.PersonInfoId
                       select i;
            return View(list); 
            
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_healthContext.Categories, "CatId", "CatId");
            ViewData["PersonInfoId"] = new SelectList(_healthContext.PersonInfos, "PersonInfoId", "PersonInfoId");
            ViewData["SpecId"] = new SelectList(_healthContext.Specializations, "SpecId", "SpecId");
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocId,SpecId,PersonInfoId,CatId")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _healthContext.Add(doctor);
                await _healthContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_healthContext.Categories, "CatId", "CatId", doctor.CatId);
            ViewData["PersonInfoId"] = new SelectList(_healthContext.PersonInfos, "PersonInfoId", "PersonInfoId", doctor.PersonInfoId);
            ViewData["SpecId"] = new SelectList(_healthContext.Specializations, "SpecId", "SpecId", doctor.SpecId);
            return View(doctor);
        }
        /*
        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _healthContext.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_healthContext.Categories, "CatId", "CatId", doctor.CatId);
            ViewData["PersonInfoId"] = new SelectList(_healthContext.PersonInfos, "PersonInfoId", "PersonInfoId", doctor.PersonInfoId);
            ViewData["SpecId"] = new SelectList(_healthContext.Specializations, "SpecId", "SpecId", doctor.SpecId);
            return View(doctor);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocId,SpecId,PersonInfoId,CatId")] Doctor doctor)
        {
            if (id != doctor.DocId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _healthContext.Update(doctor);
                    await _healthContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.DocId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_healthContext.Categories, "CatId", "CatId", doctor.CatId);
            ViewData["PersonInfoId"] = new SelectList(_healthContext.PersonInfos, "PersonInfoId", "PersonInfoId", doctor.PersonInfoId);
            ViewData["SpecId"] = new SelectList(_healthContext.Specializations, "SpecId", "SpecId", doctor.SpecId);
            return View(doctor);
        }

        private bool DoctorExists(int id)
        {
            return _healthContext.Doctors.Any(e => e.DocId == id);
        }*/
    }
}
