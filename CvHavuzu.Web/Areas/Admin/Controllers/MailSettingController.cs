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
using Microsoft.AspNetCore.Authorization;

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class MailSettingController : Controller
    {
        private Data.ApplicationDbContext context;
        private IHostingEnvironment env;
        public MailSettingController(IHostingEnvironment _env, Data.ApplicationDbContext _context)
        {
            this.context = _context;
            this.env = _env;
        }
        public IActionResult Index()
        {
            MailSetting cms;
            cms = context.MailSettings.FirstOrDefault();
            if (cms == null)
            {
                cms = new MailSetting();
                cms.FromAddress="denemecvhavuzu@gmail.com";
                cms.MailUsername="denemecvhavuzu@gmail.com";
                cms.MailPassword="123:Asdfg";
                cms.FromAddressTitle = "Cv Havuzu";
                cms.Subject = "CV Havuzu İletişim Formu - {0}"; 
                cms.BodyContent = "Mesaj�n�z Bize �letilmi�tir. �lginiz ��in Te�ekk�r Ederiz";
                cms.SmptServer = "smtp.gmail.com";
                cms.SmptPortNumber = 587;
                cms.UseSSL = false;
                cms.ToAddress = "nbu@bilisimegitim.com";
                context.Add(cms);
                context.SaveChanges();



            }
            return View(cms);
        }

        [HttpPost]
        public IActionResult Index(MailSetting mailSetting)
        {
            if (ModelState.IsValid)
            {
                MailSetting cms;
                if (context.MailSettings.Any())
                {
                    cms = context.MailSettings.FirstOrDefault();
                    cms.FromAddress = mailSetting.FromAddress;
                    cms.FromAddressTitle = mailSetting.FromAddressTitle;
                    cms.MailUsername = mailSetting.MailUsername;
                    cms.MailPassword = mailSetting.MailPassword;
                    cms.BodyContent = mailSetting.BodyContent;
                    cms.SmptPortNumber = mailSetting.SmptPortNumber;
                    cms.SmptServer = mailSetting.SmptServer;
                    cms.Subject = mailSetting.Subject;
                    cms.UseSSL = mailSetting.UseSSL;
                    cms.ToAddress = mailSetting.ToAddress;
                    context.SaveChanges();
                }
            }
            return View(mailSetting);
        }
    }
}