using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CvHavuzu.Web.Data;
using CvHavuzu.Web.Models;
using CvHavuzu.Web.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace CvHavuzu.Web.Controllers
{
    [Authorize]
    public class MyResumeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AccountController newAccountController;

        public MyResumeController(ApplicationDbContext context) : base(context)
        {
            _context = context;    
        }

        // GET: MyResume
        public async Task<IActionResult> Index()
        {
            
                var applicationDbContext = _context.Resumes.Include(r => r.Consultant).Include(r => r.Department).Include(r => r.EducationLevel).Include(r => r.Profession).Include(r => r.ResumeStatus).Include(r => r.Teacher).Include(r => r.University)/*.Where(r=>r.UserName==User.Identity.UserName)*/;
                return View(await applicationDbContext.ToListAsync());
          
        }

        // GET: MyResume/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await _context.Resumes
                .Include(r => r.Consultant)
                .Include(r => r.Department)
                .Include(r => r.EducationLevel)
                .Include(r => r.Profession)
                .Include(r => r.ResumeStatus)
                .Include(r => r.Teacher)
                .Include(r => r.University)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // GET: MyResume/Create
        public IActionResult Create()
        {
            ViewData["ConsultantId"] = new SelectList(_context.Consultants, "Id", "Fullname");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevels, "Id", "Name");
            ViewData["ProfessionId"] = new SelectList(_context.Professions, "Id", "Name");
            ViewData["ResumeStatusId"] = new SelectList(_context.ResumesStatuses, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname");
            ViewData["UniversityId"] = new SelectList(_context.Universities, "Id", "Name");
            return View();
        }

        // POST: MyResume/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Gender,ProfessionId,EducationLevelId,UniversityId,DepartmentId,BirthDate,ResumeFile,ResumeStatusId,Skills,ShowInList,Location,TeacherId,ConsultantId,Approved,CreateDate,UpdateDate")] Resume resume)
        {
            if (ModelState.IsValid)
            {
                //resume.UserName = User.Identity.Name;
                resume.Approved = false;
                _context.Add(resume);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ConsultantId"] = new SelectList(_context.Consultants, "Id", "Fullname", resume.ConsultantId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", resume.DepartmentId);
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevels, "Id", "Name", resume.EducationLevelId);
            ViewData["ProfessionId"] = new SelectList(_context.Professions, "Id", "Name", resume.ProfessionId);
            ViewData["ResumeStatusId"] = new SelectList(_context.ResumesStatuses, "Id", "Name", resume.ResumeStatusId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname", resume.TeacherId);
            ViewData["UniversityId"] = new SelectList(_context.Universities, "Id", "Name", resume.UniversityId);
            return View(resume);
        }

        // GET: MyResume/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await _context.Resumes.SingleOrDefaultAsync(m => m.Id == id);
            if (resume == null)
            {
                return NotFound();
            }
            ViewData["ConsultantId"] = new SelectList(_context.Consultants, "Id", "Fullname", resume.ConsultantId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", resume.DepartmentId);
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevels, "Id", "Name", resume.EducationLevelId);
            ViewData["ProfessionId"] = new SelectList(_context.Professions, "Id", "Name", resume.ProfessionId);
            ViewData["ResumeStatusId"] = new SelectList(_context.ResumesStatuses, "Id", "Name", resume.ResumeStatusId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname", resume.TeacherId);
            ViewData["UniversityId"] = new SelectList(_context.Universities, "Id", "Name", resume.UniversityId);
            return View(resume);
        }

        // POST: MyResume/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Gender,ProfessionId,EducationLevelId,UniversityId,DepartmentId,BirthDate,ResumeFile,ResumeStatusId,Skills,ShowInList,Location,TeacherId,ConsultantId,Approved,CreateDate,UpdateDate")] Resume resume)
        {
            if (id != resume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //resume.UserName = User.Identity.Name;
                    resume.Approved = false;
                    _context.Update(resume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResumeExists(resume.Id))
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
            ViewData["ConsultantId"] = new SelectList(_context.Consultants, "Id", "Fullname", resume.ConsultantId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", resume.DepartmentId);
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevels, "Id", "Name", resume.EducationLevelId);
            ViewData["ProfessionId"] = new SelectList(_context.Professions, "Id", "Name", resume.ProfessionId);
            ViewData["ResumeStatusId"] = new SelectList(_context.ResumesStatuses, "Id", "Name", resume.ResumeStatusId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname", resume.TeacherId);
            ViewData["UniversityId"] = new SelectList(_context.Universities, "Id", "Name", resume.UniversityId);
            return View(resume);
        }

        // GET: MyResume/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await _context.Resumes
                .Include(r => r.Consultant)
                .Include(r => r.Department)
                .Include(r => r.EducationLevel)
                .Include(r => r.Profession)
                .Include(r => r.ResumeStatus)
                .Include(r => r.Teacher)
                .Include(r => r.University)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // POST: MyResume/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resume = await _context.Resumes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ResumeExists(int id)
        {
            return _context.Resumes.Any(e => e.Id == id);
        }
    }
}
