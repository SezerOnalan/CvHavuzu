using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class StatViewModel
    {
        [Required]
        [Display(Name = "İsim Soyisim")]
        [StringLength(200)]
        public string Fullname { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        [StringLength(200)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Şirket İsmi")]
        [StringLength(200)]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Telefon Numarası")]
        [StringLength(200)]
        public string Phone { get; set; }
      
    }
}
