using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad - Soyad alanı zorunludur.")]
        [StringLength(200)]
        [Display(Name = "Ad - Soyad")]
        public string Fullname { get; set; }
    }
}
