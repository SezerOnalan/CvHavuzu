using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class ResumeStatus
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ad alanı gereklidir")]
        [StringLength(200)]
        [Display(Name ="Ad" )]
        public string Name { get; set; }
        [Display(Name ="Açıklama")]
        public string Description { get; set; }
    }
}
