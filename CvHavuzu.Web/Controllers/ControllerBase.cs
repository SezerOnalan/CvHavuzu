using CvHavuzu.Web.Data;
using CvHavuzu.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Controllers
{
    public class ControllerBase: Controller
    {
        protected ApplicationDbContext db;
        public ControllerBase(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (db.Settings.Any())
            {
                var setting = db.Settings.FirstOrDefault();
                ViewBag.Setting = setting;
                var resume = db.Resumes
                       .Include(x => x.Profession)
                    .OrderByDescending(r => r.CreateDate).Take(5).ToList();
                ViewBag.Resumes = resume;
            } else
            {
                var setting = new Setting();
                ViewBag.Setting = setting;
            }
            
        }
        
    
    }
}
