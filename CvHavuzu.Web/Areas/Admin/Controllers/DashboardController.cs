using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CvHavuzu.Web.Data;

namespace CvHavuzu.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private ApplicationDbContext context;
        public DashboardController(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult Index()
        {
            var stats = context.Stats.OrderByDescending(s => s.DownloadDate).ToList();
            return View(stats);
        }
    }
}