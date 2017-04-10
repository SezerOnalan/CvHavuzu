using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CvHavuzu.Web.Data;
using CvHavuzu.Web.Models;

namespace CvHavuzu.Web.Areas.Admin
{
    [Area("Admin")]
    public class ResumeStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResumeStatusController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Admin/ResumeStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResumesStatuses.ToListAsync());
        }

        // GET: Admin/ResumeStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resumeStatus = await _context.ResumesStatuses
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resumeStatus == null)
            {
                return NotFound();
            }

            return View(resumeStatus);
        }

        // GET: Admin/ResumeStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ResumeStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] ResumeStatus resumeStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resumeStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(resumeStatus);
        }

        // GET: Admin/ResumeStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resumeStatus = await _context.ResumesStatuses.SingleOrDefaultAsync(m => m.Id == id);
            if (resumeStatus == null)
            {
                return NotFound();
            }
            return View(resumeStatus);
        }

        // POST: Admin/ResumeStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ResumeStatus resumeStatus)
        {
            if (id != resumeStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resumeStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResumeStatusExists(resumeStatus.Id))
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
            return View(resumeStatus);
        }

        // GET: Admin/ResumeStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resumeStatus = await _context.ResumesStatuses
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resumeStatus == null)
            {
                return NotFound();
            }

            return View(resumeStatus);
        }

        // POST: Admin/ResumeStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resumeStatus = await _context.ResumesStatuses.SingleOrDefaultAsync(m => m.Id == id);
            _context.ResumesStatuses.Remove(resumeStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ResumeStatusExists(int id)
        {
            return _context.ResumesStatuses.Any(e => e.Id == id);
        }
    }
}
