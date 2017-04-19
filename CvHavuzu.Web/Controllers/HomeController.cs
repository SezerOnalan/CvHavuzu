using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CvHavuzu.Web.Data;
using CvHavuzu.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CvHavuzu.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context) : base(context)
        {
            _context = context;
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
                    .Include(x => x.Teacher).Where(r => r.ShowInList == true && r.Approved == true);

                   foreach (var term in terms)
                { 
                    resumes = resumes.Where(r => r.FirstName.ToLower().Contains(term) ||
                    r.LastName.ToLower().Contains(term) ||
                    r.Gender.ToString().ToLower().Contains(term) ||
                    r.Profession.Name.ToLower().Contains(term) ||
                    r.EducationLevel.Name.ToLower().Contains(term) ||
                    r.University.Name.ToLower().Contains(term) ||
                    r.Department.Name.ToLower().Contains(term) ||
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
