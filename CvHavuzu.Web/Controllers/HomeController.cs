using System;
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


            public IActionResult DownloadDetails(int Id)
        {
            Resume resume = new Resume();
            resume = _context.Resumes.FirstOrDefault(r => r.Id == Id);
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

        }

        public IActionResult Index(string query = "")
        {
          

            ViewBag.Query = query;
            if (String.IsNullOrEmpty(query)) {
                // query parametresinden değer gelmiyorsa tüm kayıtları getir
                var resumes = _context.Resumes
                    .Include(x => x.Department)
                    .Include(x => x.University)
                    .Include(x => x.Profession)
                    .Include(x => x.ResumeStatus)
                    .Include(x => x.Consultant)
                    .Include(x => x.EducationLevel)
                    .Include(x => x.Teacher)
                    .Include(x => x.City)
                    .Include(x => x.District)
                    .Where(r => r.ShowInList == true && r.Approved == true).ToList();
                return View(resumes);
            } else
            {
                // query'den değer geliyorsa where metoduyla filtreleme yap
                query = query.ToLower();
                string[] terms =query.Split(' ');
                var resumes = _context.Resumes
                    .Include(x => x.Department)
                    .Include(x => x.University)
                    .Include(x => x.Profession)
                    .Include(x => x.ResumeStatus)
                    .Include(x => x.Consultant)
                    .Include(x => x.EducationLevel)
                    .Include(x => x.Teacher)
                    .Include(x => x.City)
                    .Include(x => x.District)
                    .Where(r => r.ShowInList == true && r.Approved == true);

                   foreach (var term in terms)
                { 
                    resumes = resumes.Where(r => r.FirstName.ToLower().Contains(term) ||
                    r.LastName.ToLower().Contains(term) ||
                    r.Gender.ToString().ToLower().Contains(term) ||
                    r.Profession.Name.ToLower().Contains(term) ||
                    r.EducationLevel.Name.ToLower().Contains(term) ||
                    r.University.Name.ToLower().Contains(term) ||
                    r.Department.Name.ToLower().Contains(term) ||
                    r.City.Name.ToLower().Contains(term) ||
                    r.District.Name.ToLower().Contains(term) ||
                    r.Skills.ToLower().Contains(term));
                    }
                    
                return View(resumes);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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
    }
}   
