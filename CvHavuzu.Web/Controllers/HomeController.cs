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
        public IActionResult Index()
        {
            var resumes = _context.Resumes.ToList();
                resumes = _context.Resumes.Include(x => x.Department).ToList();
                resumes = _context.Resumes.Include(x => x.University).ToList();
                resumes = _context.Resumes.Include(x => x.Profession).ToList();
                resumes = _context.Resumes.Include(x => x.ResumeStatus).ToList();
                resumes = _context.Resumes.Include(x => x.Consultant).ToList();
                resumes = _context.Resumes.Include(x => x.EducationLevel).ToList();
                resumes = _context.Resumes.Include(x => x.Teacher).ToList();
                
            return View(resumes);
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
