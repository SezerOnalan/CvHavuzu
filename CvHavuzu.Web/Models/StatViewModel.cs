using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class StatViewModel
    {
        [Required(ErrorMessage = "Ad Soyad Alanı Gereklidir")]
        [Display(Name = "Ad Soyad")]
        [StringLength(200)]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "E-mail Alanı Gereklidir")]
        [Display(Name = "E-mail")]
        [StringLength(200)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Firma Adı Alanı Gereklidir")]
        [Display(Name = "Firma Adı")]
        [StringLength(200)]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Telefon Numarası Alanı Gereklidir")]
        [Display(Name = "Telefon Numarası")]
        [StringLength(200)]
        public string Phone { get; set; }
      
    }
}
