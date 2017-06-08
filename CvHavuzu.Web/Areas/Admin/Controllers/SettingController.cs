using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CvHavuzu.Web.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN")]
    public class SettingController : Controller
    {
        private Data.ApplicationDbContext context;
        private IHostingEnvironment env;
        public SettingController(IHostingEnvironment _env, Data.ApplicationDbContext _context)
        {
            this.context = _context;
            this.env = _env;
        }
        public IActionResult Index()
        {
            Setting setting;
            setting = context.Settings.FirstOrDefault();
            if (setting == null)
            {
                setting = new Setting();
            }
            return View(setting);
        }

        [HttpPost]
        public IActionResult Index(Setting setting, IFormFile logoUpload)
        {
            if (ModelState.IsValid)
            {
                Setting s;
                if (context.Settings.Any())
                {
                    s = context.Settings.FirstOrDefault();
                    s.WelcomeText = setting.WelcomeText;
                    s.MembershipAgreement = setting.MembershipAgreement;
                    s.ResumeDownloadSecurity = setting.ResumeDownloadSecurity;
                    s.LastResumeCreateDate = setting.LastResumeCreateDate;
                    s.FooterText = setting.FooterText;
                    s.CustomHtml = setting.CustomHtml;
                    s.UpdateDate = DateTime.Now;
                    s.Logo = setting.Logo;
                    s.Title = setting.Title;
                    s.SeoTitle = setting.SeoTitle;
                    s.SeoDescription = setting.SeoDescription;
                    s.SeoKeywords = setting.SeoKeywords;
                    s.Address = setting.Address;
                    s.Phone = setting.Phone;
                    s.Fax = setting.Fax;
                    s.Mail = setting.Mail;
                    s.Facebook = setting.Facebook;
                    s.Twitter = setting.Twitter;
                    s.LinkedIn = setting.LinkedIn;
                    s.About = setting.About;
                    s.PrivacyPolicy = setting.PrivacyPolicy;
                    s.TermsOfUse = setting.TermsOfUse;

                    // file upload iþlemi yapýlýr

                    if (logoUpload != null && logoUpload.Length > 0)
                    {
                        var filePath = new Random().Next(9999).ToString() + logoUpload.FileName;
                        using (var stream = new FileStream(env.WebRootPath + "\\uploads\\" + filePath, FileMode.Create))
                        {
                            logoUpload.CopyTo(stream);
                        }
                        s.Logo = filePath;
                    }
                    context.SaveChanges();
                } else
                {
                    // file upload iþlemi yapýlýr
                    if (logoUpload != null && logoUpload.Length > 0)
                    {
                        var filePath = new Random().Next(9999).ToString() + logoUpload.FileName;
                        using (var stream = new FileStream(env.WebRootPath + "\\uploads\\" + filePath, FileMode.Create))
                        {
                            logoUpload.CopyTo(stream);
                        }
                        setting.Logo = filePath;
                    }
                    context.Settings.Add(setting);
                    context.SaveChanges();
                    ViewBag.Message = "Ayarlar baþarýyla kaydedildi.";
                }
                
               
            }
            return View(setting);
        }
    }
}