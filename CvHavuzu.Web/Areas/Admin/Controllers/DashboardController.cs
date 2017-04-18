using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CvHavuzu.Web.Data;
using Microsoft.AspNetCore.Identity;
using CvHavuzu.Web.Models;

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DashboardController(ApplicationDbContext _context, UserManager<ApplicationUser> userManager)
        {
            this.context = _context;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            var stats = context.Stats.OrderByDescending(s => s.DownloadDate).ToList();
            return View(stats);
        }
        public IActionResult Users()
        {
            var users= _userManager.Users.ToList();
            return View(users);
        }
    }
}