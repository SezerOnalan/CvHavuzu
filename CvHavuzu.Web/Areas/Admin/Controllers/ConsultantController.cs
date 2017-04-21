using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CvHavuzu.Web.Data;
using CvHavuzu.Web.Models;

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ConsultantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultantController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Admin/Consultant
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consultants.ToListAsync());
        }

        // GET: Admin/Consultant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants
                .SingleOrDefaultAsync(m => m.Id == id);
            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }

        // GET: Admin/Consultant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Consultant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fullname")] Consultant consultant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultant);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(consultant);
        }

        // GET: Admin/Consultant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants.SingleOrDefaultAsync(m => m.Id == id);
            if (consultant == null)
            {
                return NotFound();
            }
            return View(consultant);
        }

        // POST: Admin/Consultant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fullname")] Consultant consultant)
        {
            if (id != consultant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultantExists(consultant.Id))
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
            return View(consultant);
        }

        // GET: Admin/Consultant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants
                .SingleOrDefaultAsync(m => m.Id == id);
            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }

        // POST: Admin/Consultant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultant = await _context.Consultants.SingleOrDefaultAsync(m => m.Id == id);
            try { 
            _context.Consultants.Remove(consultant);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Delete", "Silme Ýþlemi Esnasýnda Hata Oluþtu.Bu Kayýdýn Baþka Kayýtlar Tarafýndan Kullanýlmadýðýna Emin Olun !!");
                return View(consultant);
            }
        }

        private bool ConsultantExists(int id)
        {
            return _context.Consultants.Any(e => e.Id == id);
        }
    }
}
