using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CvHavuzu.Web.Data;
using CvHavuzu.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResumeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment env;

        private string FDir_AppData = "~/App_Data/";


        public ResumeController(IHostingEnvironment _env, ApplicationDbContext context, Data.ApplicationDbContext _contxt)
        {
            _context = context;
            this.env = _env;
        }

        // GET: Admin/Resume
        public async Task<IActionResult> Index(int tab = 1)
        {
            IList<Resume> resumes;
            if (tab == 1)
            {
                resumes = _context.Resumes.Include(r => r.Consultant).Include(r => r.Department).Include(r => r.EducationLevel).Include(r => r.Profession).Include(r => r.ResumeStatus).Include(r => r.City).Include(r => r.District).Include(r => r.Teacher).Include(r => r.University).ToList();
            }
            else if (tab == 2)
            {
                resumes = _context.Resumes.Include(r => r.Consultant).Include(r => r.Department).Include(r => r.EducationLevel).Include(r => r.Profession).Include(r => r.ResumeStatus).Include(r => r.City).Include(r => r.District).Include(r => r.Teacher).Include(r => r.University).Where(r => r.ShowInList == true && r.Approved == true).ToList();
            }
            else
            {
                resumes = _context.Resumes.Include(r => r.Consultant).Include(r => r.Department).Include(r => r.EducationLevel).Include(r => r.Profession).Include(r => r.ResumeStatus).Include(r => r.City).Include(r => r.District).Include(r => r.Teacher).Include(r => r.University).Where(r => r.Approved == false).ToList();
            }
            ViewBag.tab = tab;
            return View(resumes);
        }

        // GET: Admin/Resume/Details/5
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
                .Include(r => r.City)
                .Include(r => r.District)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // GET: Admin/Resume/Create
        public IActionResult Create()
        {
           
            ViewData["ConsultantId"] = new SelectList(_context.Consultants, "Id", "Fullname");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevels, "Id", "Name");
            ViewData["ProfessionId"] = new SelectList(_context.Professions, "Id", "Name");
            ViewData["ResumeStatusId"] = new SelectList(_context.ResumesStatuses, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname");
            ViewData["UniversityId"] = new SelectList(_context.Universities, "Id", "Name");
            return View();
        }

        // POST: Admin/Resume/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Resume resume, IFormFile imageUpload, IFormFile resumeUpload)
        {
            
            if (imageUpload != null && imageUpload.Length > 0 && ".gif,.jpg,.jpeg,.png".Contains(Path.GetExtension(imageUpload.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Resim dosyasý uzantýsý .gif, .jpg, .jpeg ya da .png olmalýdýr.");
            }
            if (resumeUpload != null && resumeUpload.Length > 0 && ".pdf,.doc,.docx".Contains(Path.GetExtension(resumeUpload.FileName)) == false || resumeUpload == null)
            {
                ModelState.AddModelError("ResumeUpload", "Cv dosyasý uzantýsý .doc, .docx ya da .pdf olmalýdýr ve alan boþ olamaz.");
            }
            else if (ModelState.IsValid)
            {
                // file upload iþlemi yapýlýr

                if (imageUpload != null && imageUpload.Length > 0)
                {

                    var filePath = new Random().Next(9999).ToString() + imageUpload.FileName;
                   
                    using (var stream = new FileStream(env.WebRootPath + "\\uploads\\resumes\\images\\" + filePath, FileMode.Create))
                    {
                        imageUpload.CopyTo(stream);
                    }
                    resume.ImagePath = filePath;

                }

                if (resumeUpload != null && resumeUpload.Length > 0)
                {
                    var filePath = new Random().Next(9999).ToString() + resumeUpload.FileName;
                    filePath = ReplaceTurksihEnglishCharacter(filePath);
                    using (var stream = new FileStream(env.WebRootPath + "\\uploads\\resumes\\" + filePath, FileMode.Create))
                    {
                        resumeUpload.CopyTo(stream);
                    }
                    resume.ResumeFile = filePath;
                }
                _context.Add(resume);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ConsultantId"] = new SelectList(_context.Consultants, "Id", "Fullname", resume.ConsultantId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", resume.DepartmentId);
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevels, "Id", "Name", resume.EducationLevelId);
            ViewData["ProfessionId"] = new SelectList(_context.Professions, "Id", "Name", resume.ProfessionId);
            ViewData["ResumeStatusId"] = new SelectList(_context.ResumesStatuses, "Id", "Name", resume.ResumeStatusId);
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", resume.CityId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Name", resume.DistrictId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname", resume.TeacherId);
            ViewData["UniversityId"] = new SelectList(_context.Universities, "Id", "Name", resume.UniversityId);
            return View(resume);
        }

        // GET: Admin/Resume/Edit/5
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", resume.CityId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Name", resume.DistrictId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname", resume.TeacherId);
            ViewData["UniversityId"] = new SelectList(_context.Universities, "Id", "Name", resume.UniversityId);
            return View(resume);
        }

        // POST: Admin/Resume/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Resume resume, IFormFile imageUpload, IFormFile resumeUpload)
        {
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
                // file edit iþlemi yapýlýr

                try
                {
                    // file upload iþlemi yapýlýr

                    if (imageUpload != null && imageUpload.Length > 0)
                    {
                        var filePath = new Random().Next(9999).ToString() + imageUpload.FileName;
                        using (var stream = new FileStream(env.WebRootPath + "\\uploads\\resumes\\images\\" + filePath, FileMode.Create))
                        {
                            imageUpload.CopyTo(stream);
                        }
                        resume.ImagePath = filePath;
                    }




                    if (resumeUpload != null && resumeUpload.Length > 0)
                    {
                        var filePath = new Random().Next(9999).ToString() + resumeUpload.FileName;

                        filePath= ReplaceTurksihEnglishCharacter(filePath);

                        using (var stream = new FileStream(env.WebRootPath + "\\uploads\\resumes\\" + filePath, FileMode.Create))
                        {
                            resumeUpload.CopyTo(stream);
                        }
                        resume.ResumeFile = filePath;
                    }



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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", resume.CityId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Name", resume.DistrictId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname", resume.TeacherId);
            ViewData["UniversityId"] = new SelectList(_context.Universities, "Id", "Name", resume.UniversityId);
            return View(resume);
        }

        // GET: Admin/Resume/Delete/5
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

        // POST: Admin/Resume/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resume = await _context.Resumes.SingleOrDefaultAsync(m => m.Id == id);

            try
            {
                _context.Resumes.Remove(resume);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Delete", "Silme Ýþlemi Esnasýnda Hata Oluþtu.Bu Kayýdýn Baþka Kayýtlar Tarafýndan Kullanýlmadýðýna Emin Olun !!");
                return View(resume);
            }
        }

        private bool ResumeExists(int id)
        {
            return _context.Resumes.Any(e => e.Id == id);
        }

        public static string  ReplaceTurksihEnglishCharacter(string FileName)
        {
            FileName = FileName.Replace('ö', 'o');
            FileName = FileName.Replace('ü', 'u');
            FileName = FileName.Replace('ð', 'g');
            FileName = FileName.Replace('þ', 's');
            FileName = FileName.Replace('ý', 'i');
            FileName = FileName.Replace('ç', 'c');
            FileName = FileName.Replace('Ö', 'O');
            FileName = FileName.Replace('Ü', 'U');
            FileName = FileName.Replace('Ð', 'G');
            FileName = FileName.Replace('Þ', 'S');
            FileName = FileName.Replace('Ý', 'I');
            FileName = FileName.Replace('Ç', 'C');
            return FileName;
        }
    }
}
