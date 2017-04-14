using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class District
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "İlçe")]
        public string Name { get; set; }
        [Display(Name = "Şehir Id")]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        [Display(Name = "Şehir")]
        public virtual City City { get; set; }

    }
}
