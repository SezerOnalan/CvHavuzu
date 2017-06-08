using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CvHavuzu.Web.Data;
using CvHavuzu.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class EducationLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationLevelsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Admin/EducationLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.EducationLevels.ToListAsync());
        }

        // GET: Admin/EducationLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevels
                .SingleOrDefaultAsync(m => m.Id == id);
            if (educationLevel == null)
            {
                return NotFound();
            }

            return View(educationLevel);
        }

        // GET: Admin/EducationLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/EducationLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] EducationLevel educationLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(educationLevel);
        }

        // GET: Admin/EducationLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevels.SingleOrDefaultAsync(m => m.Id == id);
            if (educationLevel == null)
            {
                return NotFound();
            }
            return View(educationLevel);
        }

        // POST: Admin/EducationLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] EducationLevel educationLevel)
        {
            if (id != educationLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationLevelExists(educationLevel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(educationLevel);
        }

        // GET: Admin/EducationLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevels
                .SingleOrDefaultAsync(m => m.Id == id);
            if (educationLevel == null)
            {
                return NotFound();
            }

            return View(educationLevel);
        }

        // POST: Admin/EducationLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationLevel = await _context.EducationLevels.SingleOrDefaultAsync(m => m.Id == id);
            try { 
            _context.EducationLevels.Remove(educationLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Delete", "Silme Ýþlemi Esnasýnda Hata Oluþtu.Bu Kayýdýn Baþka Kayýtlar Tarafýndan Kullanýlmadýðýna Emin Olun !!");
                return View(educationLevel);
            }
        }

        private bool EducationLevelExists(int id)
        {
            return _context.EducationLevels.Any(e => e.Id == id);
        }
    }
}
