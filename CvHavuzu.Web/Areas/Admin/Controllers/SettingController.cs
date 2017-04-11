using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CvHavuzu.Web.Models;

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private Data.ApplicationDbContext context;
        public SettingController(Data.ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult Index()
        {
            Setting setting = new Setting();
            setting.WelcomeText = "Örnek metin";
            return View(setting);
        }
    }
}