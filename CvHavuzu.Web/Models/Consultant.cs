using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class Consultant
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]

        [Display(Name ="isim")]
        public string Fullname { get; set; }
    }
}
