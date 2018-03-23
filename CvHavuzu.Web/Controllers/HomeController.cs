﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CvHavuzu.Web.Data;
using CvHavuzu.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using PagedList.Core;
using Microsoft.AspNetCore.Http;

namespace CvHavuzu.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment env;
        public HomeController(ApplicationDbContext context, IHostingEnvironment _env) : base(context)
        {
            _context = context;
            this.env = _env;
        }

        public IActionResult HideInList(int Id)
        {
            Resume resume = new Resume();
            resume = _context.Resumes.FirstOrDefault(r => r.Id == Id);
            resume.ShowInList = false;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }


            public IActionResult DownloadResume(int Id)
            {
            if (((Setting)ViewBag.Setting).ResumeDownloadSecurity == ResumeDownloadSecurity.FreeDownload || ((Setting)ViewBag.Setting).ResumeDownloadSecurity == ResumeDownloadSecurity.MembershipRequired && User.Identity.IsAuthenticated) 
            { 
                Resume resume = _context.Resumes.FirstOrDefault(r => r.Id == Id);
                Stat stat = new Stat();
                stat.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                stat.DownloadDate = DateTime.Now;
                stat.ResumeId =Id;
                stat.ResumeFullName = resume.FirstName + " " + resume.LastName;
                _context.Add(stat);
                _context.SaveChanges();

                string filename = resume.ResumeFile;
                string filepath = env.WebRootPath + "\\uploads\\resumes\\" + filename;
                byte[] filedata = System.IO.File.ReadAllBytes(filepath);

                Response.Headers.Add("Content-Disposition", $"inline; filename={filename}");
                return File(filedata, "application/octet-stream");
            } else if (((Setting)ViewBag.Setting).ResumeDownloadSecurity == ResumeDownloadSecurity.NamePhoneEmailRequired)
            {
                if (HttpContext.Session.GetString("Fullname") == null)
                {
                    return View("EnterNamePhoneEmail", new StatViewModel());
                }

                Resume resume = _context.Resumes.FirstOrDefault(r => r.Id == Id);              
                var s = new Stat();
                s.Fullname = HttpContext.Session.GetString("Fullname");
                s.Email = HttpContext.Session.GetString("Email");
                s.Phone = HttpContext.Session.GetString("Phone");
                s.CompanyName = HttpContext.Session.GetString("CompanyName");
                s.ResumeId = Id;
                s.DownloadDate = DateTime.Now;
                s.ResumeFullName = resume.FirstName + " " + resume.LastName;
                s.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.Add(s);
                _context.SaveChanges();
                string filename = resume.ResumeFile;
                string filepath = env.WebRootPath + "\\uploads\\resumes\\" + filename;
                byte[] filedata = System.IO.File.ReadAllBytes(filepath);

                Response.Headers.Add("Content-Disposition", $"inline; filename={filename}");
                return File(filedata, "application/octet-stream");

            }
            return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        public IActionResult DownloadResume(int Id, StatViewModel stat)
        {
            
            if (((Setting)ViewBag.Setting).ResumeDownloadSecurity == ResumeDownloadSecurity.NamePhoneEmailRequired)
            {
                Resume resume = _context.Resumes.FirstOrDefault(r => r.Id == Id);                             
                HttpContext.Session.SetString("Fullname", stat.Fullname);
                HttpContext.Session.SetString("Email", stat.Email);
                HttpContext.Session.SetString("Phone", stat.Phone);
                HttpContext.Session.SetString("CompanyName", stat.CompanyName);
                var s = new Stat();
                s.Fullname = stat.Fullname;
                s.Email = stat.Email;
                s.Phone = stat.Phone;
                s.CompanyName = stat.CompanyName;
                s.ResumeId = Id;
                s.DownloadDate = DateTime.Now;
                s.ResumeFullName = resume.FirstName + " " + resume.LastName;
                s.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.Add(s);
                _context.SaveChanges();
                string filename = resume.ResumeFile;
                string filepath = env.WebRootPath + "\\uploads\\resumes\\" + filename;
                byte[] filedata = System.IO.File.ReadAllBytes(filepath);

                Response.Headers.Add("Content-Disposition", $"inline; filename={filename}");
                return File(filedata, "application/octet-stream");
               
            }
            return NotFound();
        }

        public IActionResult Index(int? ProfessionId, int? EducationLevelId, int? UniversityId, int? DepartmentId, int? CityId, int? DistrictId, int Age1 = 20, int Age2 = 35, string skills = "", string gender = "", string query = "", int page = 1, int sirala = 0)
        {
           
                ViewBag.Query = query;
            ViewBag.Sirala = sirala;
            skills = skills ?? "";
            skills = skills.ToLower();
            var resumes = _context.Resumes.Include(x => x.Department)
                .Include(x => x.University)
                .Include(x => x.Profession)
                .Include(x => x.ResumeStatus)
                .Include(x => x.Consultant)
                .Include(x => x.EducationLevel)
                .Include(x => x.Teacher)
                .Include(x => x.City)
                .Include(x => x.District).Where(r => r.ShowInList == true && r.Approved == true).Where(r =>
                (!string.IsNullOrEmpty(skills) && !string.IsNullOrEmpty(r.Skills) ? r.Skills.ToLower().Contains(skills) : true) &&
                (gender=="1"?r.Gender == Gender.Erkek:(gender=="2"?r.Gender==Gender.Kadın:true)) &&
                (ProfessionId.HasValue ? r.ProfessionId == ProfessionId : true) &&
                (EducationLevelId.HasValue  ? r.EducationLevelId == EducationLevelId : true) &&
                (UniversityId.HasValue ? r.UniversityId == UniversityId : true) &&
                (DepartmentId.HasValue ? r.DepartmentId == DepartmentId : true) &&
                (CityId.HasValue? r.CityId  == CityId : true) &&
                (DistrictId.HasValue ? r.DistrictId == DistrictId : true) &&
                (r.BirthDate.HasValue ? Age1<(DateTime.Now.Year - r.BirthDate.Value.Year) && (DateTime.Now.Year - r.BirthDate.Value.Year)<Age2:true));

                if (String.IsNullOrEmpty(query))
                {
                // query parametresinden değer gelmiyorsa tüm kayıtları getir
                
                    
                    resumes = resumes.OrderByDescending(i => i.UpdateDate);
                }
                else
                {
                    // query'den değer geliyorsa where metoduyla filtreleme yap
                    query = query.ToLower();
                    string[] terms = query.Split(' ');
                    

                    foreach (var term in terms)
                    {
                        resumes = resumes.Where(r =>
                        r.FirstName.ToLower().Contains(term) ||
                        r.LastName.ToLower().Contains(term) ||
                        r.Profession.Name.ToLower().Contains(term) ||
                        r.EducationLevel.Name.ToLower().Contains(term) ||
                        r.University.Name.ToLower().Contains(term) ||
                        r.Department.Name.ToLower().Contains(term) ||
                        r.City.Name.ToLower().Contains(term) ||
                        r.District.Name.ToLower().Contains(term) ||
                        r.Skills.ToLower().Contains(term));
                    }

                    resumes = resumes.OrderByDescending(i => i.UpdateDate);
                }

            /* 0:Güncel olan önce
             * 1:Ada göre artan
             * 2:Ada göre azalan
             * 3:Konuma göre artan
             * 4:Konuma göre azalan
             * 5:Üniversite adına göre artan
             * 6:Üniversite adına göre azalan
             * */

            switch (sirala)
            {
                case 0:
                    break;
                case 1:
                    resumes = resumes.OrderBy(x => x.FirstName).ThenBy(y=>y.LastName);
                    break;
                case 2:
                    resumes = resumes.OrderByDescending(x => x.FirstName).ThenByDescending(y => y.LastName);
                    break;
                case 3:
                    resumes = resumes.OrderBy(x => x.City.Name).ThenBy(y => y.District.Name);
                    break;
                case 4:
                    resumes = resumes.OrderByDescending(x => x.City.Name).ThenByDescending(y => y.District.Name);
                    break;
                case 5:
                    resumes = resumes.OrderBy(x => x.University.Name).ThenBy(y=>y.Department.Name);
                    break;
                case 6:
                    resumes = resumes.OrderByDescending(x => x.University.Name).ThenByDescending(y => y.Department.Name);
                    break;

            }
            ViewBag.Age1 = Age1;
            ViewBag.Age2 = Age2;
            ViewBag.Skills = skills;
            ViewBag.Gender = gender;
            ViewBag.ProfessionId = ProfessionId;
            ViewBag.EducationLevelId = EducationLevelId;
            ViewBag.UniversityId = UniversityId;
            ViewBag.DepartmentId = DepartmentId;
            ViewBag.CityId = CityId;
            ViewBag.DistrictId = DistrictId;
            var result = resumes.ToPagedList<Resume>(page, 10);
            var p = _context.Resumes.Include(r=>r.Profession).Where(w=>w.Profession!=null).Select(s=>s.Profession).Distinct().ToList();
            ViewBag.Professions = p;
            ViewBag.EducationLevels = _context.Resumes.Include(r => r.EducationLevel).Where(w => w.EducationLevel != null).Select(s => s.EducationLevel).Distinct().ToList();
            ViewBag.Universities = _context.Resumes.Include(r => r.University).Where(w => w.University != null).Select(s => s.University).Distinct().ToList();
            ViewBag.Departments = _context.Resumes.Include(r => r.Department).Where(w => w.Department != null).Select(s => s.Department).Distinct().ToList();
            ViewBag.Cities = _context.Resumes.Include(r => r.City).Where(w => w.City != null).Select(s => s.City).Distinct().ToList();
            ViewBag.Districts = _context.Resumes.Include(r => r.District).Where(w => w.District != null).Select(s => s.District).Distinct().ToList();
            return View(result);

           
        }
        [Route("hakkinda")]
        public IActionResult About()
        {
            return View();
        }


        [HttpPost]
        [Route("iletisim")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Contact contact)
        {
            contact.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();

                MailSetting mailSetting;
                mailSetting = _context.MailSettings.FirstOrDefault();
                string FromAddress = mailSetting.FromAddress;
                string FromAddressTitle = mailSetting.FromAddressTitle;

                string ToAddress = contact.Email;
                string ToAddressTitle = contact.FullName;
                string Subject = mailSetting.Subject;
                string BodyContent = mailSetting.BodyContent;

                string SmptServer = mailSetting.SmptServer;
                int SmptPortNumber = mailSetting.SmptPortNumber;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAddressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAddressTitle, ToAddress));
                mimeMessage.Subject = Subject;
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmptServer, SmptPortNumber, false);
                    client.Authenticate(mailSetting.FromAddress, mailSetting.FromAddressPassword);
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
                ViewBag.Message = "Mesajınız başarıyla gönderildi.";

            }

            return View(contact);

        }

        [Route("iletisim")]
        public IActionResult Contact()
        {

            var contact = new Contact();
            contact.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            return View(contact);
        }

        public IActionResult Error()
        {
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            ViewData["Message"] = "Your application Privacy Policy page.";

            return View();
        }
        public IActionResult TermsOfUse()
        {
            ViewData["Message"] = "Your application Terms Of Use page.";

            return View();
        }

        public IActionResult PersonalDataUsage() {
            return View();
        }

    }
}   
