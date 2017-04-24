using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class Setting
    {
        public Setting()
        {
            ResumeDownloadSecurity = ResumeDownloadSecurity.MembershipRequired;
            UpdateDate = DateTime.Now;
        }
        public int Id { get; set; }
        [Display(Name="Hoşgeldiniz Metni")]
        public string WelcomeText { get; set; }
        [Display(Name="Üyelik Sözleşmesi")]
        public string MembershipAgreement { get; set; }
        [Display(Name="Özgeçmiş İndirme Güvenliği")]
        public ResumeDownloadSecurity ResumeDownloadSecurity { get; set; }
        [Display(Name="Son Özgeçmiş Oluşturulma Tarihi")]
        public DateTime? LastResumeCreateDate { get; set; }
        [Display(Name="Alt Bölüm Metni")]
        public string FooterText { get; set; }
        [Display(Name="Özel Html")]
        public string CustomHtml { get; set; }
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "SEO Başlık")]
        public string SeoTitle { get; set; }
        [Display(Name = "SEO Açıklama")]
        public string SeoDescription { get; set; }
        [Display(Name = "Anahtar Kelimeler")]
        public string SeoKeywords { get; set; }
        public string Logo { get; set; }
        [Display(Name = "Güncellenme Tarihi")]
        public DateTime UpdateDate { get; set; }
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Faks")]
        public string Fax { get; set; }
        [Display(Name = "Mail")]
        public string Mail { get; set; }
        [Display(Name = "Hakkında")]
        public string About { get; set; }
        [Display(Name = "Gizlilik Prensipleri")]
        public string PrivacyPolicy { get; set; }
        [Display(Name = "Kullanım Koşulları")]
        public string TermsOfUse { get; set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }


    }
}
