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
        public DateTime UpdateDate { get; set; }
    }
}
