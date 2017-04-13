using CvHavuzu.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            var setting = db.Settings.FirstOrDefault();
            ViewBag.Setting = setting;
        }
        
    
    }
}
