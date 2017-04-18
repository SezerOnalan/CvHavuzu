using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CvHavuzu.Web.Data;
using Microsoft.AspNetCore.Identity;
using CvHavuzu.Web.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Details(string id)
        {
            

            var user = await context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public async Task<IActionResult> Delete(string id)
        {
            

            var user = await context.Users
              
                .SingleOrDefaultAsync(m=>m.Id==id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Resume/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await context.Users.SingleOrDefaultAsync(m => m.Id == id.ToString());
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return RedirectToAction("Users");
        }

        private bool ResumeExists(string id)
        {
            return context.Users.Any(e => e.Id == id);
        }
    }
}