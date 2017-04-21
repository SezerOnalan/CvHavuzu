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
        public static void Seed(this ApplicationDbContext context)
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
            context.MailSettings.Add(cms);
            context.SaveChanges();
        }
        public static void AddSettings(ApplicationDbContext context)
        {
            var s = new Setting();
            s.Title = "Nitelikli Bilişim Uzmanı CV Havuzu";
            s.SeoTitle = s.Title;
            s.SeoDescription = s.Title;
            s.SeoKeywords = "cv, özgeçmiş, mühendis";
            // diğer ayarlar yazılır
            context.Settings.Add(s);
            context.SaveChanges();
        }
    }
}
