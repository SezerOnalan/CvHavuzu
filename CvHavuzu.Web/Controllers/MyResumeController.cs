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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace CvHavuzu.Web.Controllers
{
    [Authorize]
    public class MyResumeController : ControllerBase
    {
  
        private IHostingEnvironment env;

        

        public MyResumeController(IHostingEnvironment _env, ApplicationDbContext context) : base(context)
        {
            this.env = _env;
        }

        // GET: MyResume
        public async Task<IActionResult> Index()
        {
            
                var applicationDbContext = db.Resumes.Include(r => r.Consultant).Include(r => r.Department).Include(r => r.EducationLevel).Include(r => r.Profession).Include(r => r.ResumeStatus).Include(r => r.Teacher).Include(r => r.University).Include(r=>r.City).Include(r=>r.District).Where(r=>r.UserName==User.Identity.Name);
                return View(await applicationDbContext.ToListAsync());
          
        }

        // GET: MyResume/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await db.Resumes
                .Include(r => r.Consultant)
                .Include(r => r.Department)
                .Include(r => r.EducationLevel)
                .Include(r => r.Profession)
                .Include(r => r.ResumeStatus)
                .Include(r => r.Teacher)
                .Include(r => r.University)
                .Include(r => r.City)
                .Include(r => r.District)
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

            ViewData["ConsultantId"] = new SelectList(db.Consultants, "Id", "Fullname");
            ViewData["DepartmentId"] = new SelectList(db.Departments, "Id", "Name");
            ViewData["EducationLevelId"] = new SelectList(db.EducationLevels, "Id", "Name");
            ViewData["ProfessionId"] = new SelectList(db.Professions, "Id", "Name");
            ViewData["ResumeStatusId"] = new SelectList(db.ResumesStatuses, "Id", "Name");
            ViewData["CityId"] = new SelectList(db.Cities, "Id", "Name");
            ViewData["DistrictId"] = new SelectList(db.Districts, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(db.Teachers, "Id", "Fullname");
            ViewData["UniversityId"] = new SelectList(db.Universities, "Id", "Name");
            return View();
        }

        // POST: MyResume/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Resume resume, IFormFile imageUpload, IFormFile resumeUpload)
        {
            resume.UserName = User.Identity.Name;

            if (imageUpload != null && imageUpload.Length > 0 && ".gif,.jpg,.jpeg,.png".Contains(Path.GetExtension(imageUpload.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Resim dosyasý uzantýsý .gif, .jpg, .jpeg ya da .png olmalýdýr ve alan boþ olamaz");
            }
            if (resumeUpload != null && resumeUpload.Length > 0 && ".pdf,.doc,.docx".Contains(Path.GetExtension(resumeUpload.FileName)) == false || resumeUpload == null)
            {
                ModelState.AddModelError("ResumeUpload", "Cv dosyasý uzantýsý .doc, .docx ya da .pdf olmalýdýr ve alan boþ olamaz");
            }
            else if (ModelState.IsValid)
            {
                resume.Approved = false;
                resume.ShowInList = false;
                // file upload iþlemi yapýlýr

                if (imageUpload != null && imageUpload.Length > 0)
                {
                    var filePath = new Random().Next(9999).ToString() + imageUpload.FileName;
                    filePath = Areas.Admin.Controllers.ResumeController.ReplaceTurksihEnglishCharacter(filePath);
                    using (var stream = new FileStream(env.WebRootPath + "\\uploads\\resumes\\images\\" + filePath, FileMode.Create))
                    {
                        imageUpload.CopyTo(stream);
                    }
                    resume.ImagePath = filePath;
                }


                if (resumeUpload != null && resumeUpload.Length > 0)
                {
                    var filePath = new Random().Next(9999).ToString() + resumeUpload.FileName;
                    filePath = Areas.Admin.Controllers.ResumeController.ReplaceTurksihEnglishCharacter(filePath);
                    using (var stream = new FileStream(env.WebRootPath + "\\uploads\\resumes\\" + filePath, FileMode.Create))
                    {
                        resumeUpload.CopyTo(stream);
                    }
                    resume.ResumeFile = filePath;
                }
                db.Add(resume);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ConsultantId"] = new SelectList(db.Consultants, "Id", "Fullname", resume.ConsultantId);
            ViewData["DepartmentId"] = new SelectList(db.Departments, "Id", "Name", resume.DepartmentId);
            ViewData["EducationLevelId"] = new SelectList(db.EducationLevels, "Id", "Name", resume.EducationLevelId);
            ViewData["ProfessionId"] = new SelectList(db.Professions, "Id", "Name", resume.ProfessionId);
            ViewData["ResumeStatusId"] = new SelectList(db.ResumesStatuses, "Id", "Name", resume.ResumeStatusId);
            ViewData["CityId"] = new SelectList(db.Cities, "Id", "Name", resume.CityId);
            ViewData["DistrictId"] = new SelectList(db.Districts, "Id", "Name", resume.DistrictId);
            ViewData["TeacherId"] = new SelectList(db.Teachers, "Id", "Fullname", resume.TeacherId);
            ViewData["UniversityId"] = new SelectList(db.Universities, "Id", "Name", resume.UniversityId);
            return View(resume);
        }
        
        // GET: MyResume/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var resume = await db.Resumes.SingleOrDefaultAsync(m => m.Id == id && m.UserName==User.Identity.Name);
            var resume = await db.Resumes.Where(m => m.UserName == User.Identity.Name).SingleOrDefaultAsync(m => m.Id == id);
            if (resume == null)
            {
                return NotFound();
            }
            ViewData["ConsultantId"] = new SelectList(db.Consultants, "Id", "Fullname", resume.ConsultantId);
            ViewData["DepartmentId"] = new SelectList(db.Departments, "Id", "Name", resume.DepartmentId);
            ViewData["EducationLevelId"] = new SelectList(db.EducationLevels, "Id", "Name", resume.EducationLevelId);
            ViewData["ProfessionId"] = new SelectList(db.Professions, "Id", "Name", resume.ProfessionId);
            ViewData["ResumeStatusId"] = new SelectList(db.ResumesStatuses, "Id", "Name", resume.ResumeStatusId);
            ViewData["CityId"] = new SelectList(db.Cities, "Id", "Name", resume.CityId);
            ViewData["DistrictId"] = new SelectList(db.Districts, "Id", "Name", resume.DistrictId);
            ViewData["TeacherId"] = new SelectList(db.Teachers, "Id", "Fullname", resume.TeacherId);
            ViewData["UniversityId"] = new SelectList(db.Universities, "Id", "Name", resume.UniversityId);
            return View(resume);
        }

        // POST: MyResume/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Resume resume, IFormFile imageUpload, IFormFile resumeUpload)
        {
           
            resume.UserName = User.Identity.Name;
            resume.UpdateDate = DateTime.Now;

            if (id != resume.Id)
            {
                return NotFound();
            }
            
            if (imageUpload != null && imageUpload.Length > 0 && ".gif,.jpg,.jpeg,.png".Contains(Path.GetExtension(imageUpload.FileName)) == false)
            {
                ModelState.AddModelError("ImageEdit", "Resim dosya uzantýsý .gif, .jpg, .jpeg ya da .png olmalýdýr.");
            }
            if (resumeUpload != null && resumeUpload.Length > 0 && ".pdf,.doc,.docx".Contains(Path.GetExtension(resumeUpload.FileName)) == false)
            {
                ModelState.AddModelError("ResumeEdit", "Cv dosya uzantýsý .doc, .docx ya da .pdf olmalýdýr.");
            } 
            else if (ModelState.IsValid)
            {
                try
                {
                    // file upload iþlemi yapýlýr

                    if (imageUpload != null && imageUpload.Length > 0)
                    {
                        var filePath = new Random().Next(9999).ToString() + imageUpload.FileName;
                        filePath = Areas.Admin.Controllers.ResumeController.ReplaceTurksihEnglishCharacter(filePath);
                        using (var stream = new FileStream(env.WebRootPath + "\\uploads\\resumes\\images\\" + filePath, FileMode.Create))
                        {
                            imageUpload.CopyTo(stream);
                        }
                        resume.ImagePath = filePath;
                    }


                    if (resumeUpload != null && resumeUpload.Length > 0)
                    {
                        var filePath = new Random().Next(9999).ToString() + resumeUpload.FileName;
                        filePath = Areas.Admin.Controllers.ResumeController.ReplaceTurksihEnglishCharacter(filePath);
                        using (var stream = new FileStream(env.WebRootPath + "\\uploads\\resumes\\" + filePath, FileMode.Create))
                        {
                            resumeUpload.CopyTo(stream);
                        }                        
                        resume.ResumeFile = filePath;
                    }

                    resume.Approved = false;
                    resume.ShowInList = false;

                    var resume2 = db.Resumes.FirstOrDefault(r => r.Id == id);
                    resume2.FirstName = resume.FirstName;
                    resume2.LastName = resume.LastName;
                    resume2.ProfessionId = resume.ProfessionId;
                    resume2.ResumeFile = resume.ResumeFile;
                    resume2.Skills = resume.Skills;
                    resume2.ShowInList = resume.ShowInList;
                    resume2.Approved = resume.Approved;
                    resume2.BirthDate = resume.BirthDate;
                    resume2.CityId = resume.CityId;
                    resume2.ConsultantId = resume.ConsultantId;
                    resume2.CreateDate = resume.CreateDate;
                    resume2.DepartmentId = resume.DepartmentId;
                    resume2.DistrictId = resume.DistrictId;
                    resume2.EducationLevelId = resume.EducationLevelId;
                    resume2.ResumeStatusId = resume.ResumeStatusId;
                    resume2.TeacherId = resume.TeacherId;
                    resume2.UniversityId = resume.UniversityId;
                    resume2.UpdateDate = resume.UpdateDate;
                    resume2.UserName = resume.UserName;
                    resume2.Gender = resume.Gender;
                    resume2.ImagePath = resume.ImagePath;
                    resume2.ResumeStatusId = resume.ResumeStatusId;

                    db.SaveChanges();
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
            ViewData["ConsultantId"] = new SelectList(db.Consultants, "Id", "Fullname", resume.ConsultantId);
            ViewData["DepartmentId"] = new SelectList(db.Departments, "Id", "Name", resume.DepartmentId);
            ViewData["EducationLevelId"] = new SelectList(db.EducationLevels, "Id", "Name", resume.EducationLevelId);
            ViewData["ProfessionId"] = new SelectList(db.Professions, "Id", "Name", resume.ProfessionId);
            ViewData["ResumeStatusId"] = new SelectList(db.ResumesStatuses, "Id", "Name", resume.ResumeStatusId);
            ViewData["CityId"] = new SelectList(db.Cities, "Id", "Name", resume.CityId);
            ViewData["DistrictId"] = new SelectList(db.Districts, "Id", "Name", resume.DistrictId);
            ViewData["TeacherId"] = new SelectList(db.Teachers, "Id", "Fullname", resume.TeacherId);
            ViewData["UniversityId"] = new SelectList(db.Universities, "Id", "Name", resume.UniversityId);
            return View(resume);
        }

        // GET: MyResume/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = await db.Resumes
                .Include(r => r.Consultant)
                .Include(r => r.Department)
                .Include(r => r.EducationLevel)
                .Include(r => r.Profession)
                .Include(r => r.ResumeStatus)
                .Include(r => r.City)
                .Include(r => r.District)
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
            var resume = await db.Resumes.SingleOrDefaultAsync(m => m.Id == id);
            db.Resumes.Remove(resume);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ResumeExists(int id)
        {
            return db.Resumes.Any(e => e.Id == id);
        }

        public IActionResult Resumes(int id)
        {

            var resume = db.Resumes.SingleOrDefaultAsync(m => m.Id == id);
            return View(resume);
        }
    }
}
