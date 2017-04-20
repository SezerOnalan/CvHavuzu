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

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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
                    cms.FromAddressPassword = mailSetting.FromAddressPassword;
                    cms.BodyContent = mailSetting.BodyContent;
                    cms.SmptPortNumber = mailSetting.SmptPortNumber;
                    cms.SmptServer = mailSetting.SmptServer;
                    cms.Subject = mailSetting.Subject;
                    context.SaveChanges();
                }
            }
            return View(mailSetting);
        }
    }
}