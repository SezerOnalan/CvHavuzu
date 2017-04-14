using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CvHavuzu.Web.Models;

namespace CvHavuzu.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<ResumeStatus> ResumesStatuses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Stat> Stats { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<CvHavuzu.Web.Models.City> City { get; set; }

        public DbSet<CvHavuzu.Web.Models.District> District { get; set; }
    }
}
