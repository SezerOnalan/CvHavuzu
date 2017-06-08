using CvHavuzu.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Data
{
    public static class ApplicationDbContextSeed
    {


        public static void Seed(this ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            // migration'ları veritabanına uygula
            context.Database.Migrate();

            // Look for any mailsettings record.
            if (context.MailSettings.Any())
            {
                return;   // DB has been seeded
            }


            // Perform seed operations
            AddMailSettings(context);
            AddSettings(context);
            AddUsers(userManager);
            AddRoles(roleManager);
            AddRoleToUser(userManager);

        }



        private static ApplicationUser user;

        private static void AddUsers(UserManager<ApplicationUser> _userManager)
        {
            user = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "denemecvhavuzu@gmail.com", Email = "denemecvhavuzu@gmail.com", EmailConfirmed = true, NormalizedEmail = "DENEMECVHAVUZU@GMAİL.COM", NormalizedUserName = "DENEMECVHAVUZU@GMAİL.COM" };
            var task1 = Task.Run(() => _userManager.CreateAsync(user, "123:Asd"));
            task1.Wait();
        }

        private static void AddRoles(RoleManager<Role> _roleManager)
        {
            string[] roles = { "ADMIN"};
            string[] stamp = { "Yönetici"};

            for (int i = 0; i < roles.Count(); i++)
            {
                var role = new Role { Id = Guid.NewGuid().ToString(), Name = roles[i], NormalizedName = roles[i], ConcurrencyStamp = stamp[i] };
                var task1 = Task.Run(() => _roleManager.CreateAsync(role));
                task1.Wait();
            }
        }
        private static void AddRoleToUser(UserManager<ApplicationUser> _userManager)
        {
            var task1 = Task.Run(() => _userManager.AddToRoleAsync(user, "ADMIN"));
            task1.Wait();
        }


        public static void AddMailSettings(ApplicationDbContext context)
        {
            // yeni bir mail setting kaydı oluşturup veritabanına ekle
            var cms = new MailSetting();
            cms.FromAddress = "denemecvhavuzu@gmail.com";
            cms.FromAddressPassword = "123:Asdfg";
            cms.FromAddressTitle = "Cv Havuzu";
            cms.Subject = "İletişim";
            cms.BodyContent = "Mesajınız Bize İletilmiştir. İlginiz İçin Teşekkür Ederiz";
            cms.SmptServer = "smtp.gmail.com";
            cms.SmptPortNumber = 587;
            cms.UseSSL = false;
            context.MailSettings.Add(cms);
            context.SaveChanges();
        }
        public static void AddSettings(ApplicationDbContext context)
        {
            var s = new Setting();
            s.Title = "Nitelikli Bilişim Uzmanı CV Havuzu";
            s.SeoTitle = s.Title;
            s.SeoDescription = s.Title;
            s.Logo = "logo.png";
            s.SeoKeywords = "cv, özgeçmiş, mühendis";
            s.Address = "Bahariye";
            s.Phone = "02122121212";
            s.Fax = "02122122121";
            s.Mail = "ornek@mail.com";
            // diğer ayarlar yazılır
            context.Settings.Add(s);
            context.SaveChanges();
        }
    }
}
