using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public enum Gender
    {
        [Display(Name = "Erkek")]
        Male = 1,

        [Display(Name = "Kadın")]
        Female = 2
    }
}
