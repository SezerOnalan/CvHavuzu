using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Şehir")]
        public string Name { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
