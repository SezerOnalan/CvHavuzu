using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class University
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Üniversite Adı girilmesi gereklidir.")]
        [StringLength(200)]
        [Display(Name="Üniversite Adı")]
        public string Name { get; set; }
    }
}
