using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class MailSetting
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Gönderen Mail Adresi")]
        public string FromAddress { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Gönderen Mail Şifresi")]
        public string FromAddressPassword { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Gönderen Adı")]
        public string FromAddressTitle { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Mailin Konusu")]
        public string Subject { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Mailin İçeriği")]
        public string BodyContent { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Mail Server Adresi")]
        public string SmptServer { get; set; }
        [Required]
        [Display(Name = "Port Numarası")]
        public int SmptPortNumber { get; set; }
    }
}
