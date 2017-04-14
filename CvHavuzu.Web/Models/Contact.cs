using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Display(Name ="Ad Soyad")]
        [Required]
        [StringLength(200)]
        public string FullName { get; set; }
        [Display(Name = "Telefon")]
        [Required]
        [StringLength(200)]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [Display(Name = "Mesaj")]
        [Required]
        public string Message { get; set; }
        [Required]
        [StringLength(200)]
        public string Ip { get; set; }

    }
}
