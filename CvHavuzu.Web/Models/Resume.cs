using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public class Resume
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(200)]
        public string LastName { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public int? ProfessionId { get; set; }
        [ForeignKey("ProfessionId")]
        public virtual Profession Profession { get; set; }

        public int? EducationLevelId { get; set; }
        [ForeignKey("EducationLevelId")]
        public virtual EducationLevel EducationLevel { get; set; }

        public int? UniversityId { get; set; }
        [ForeignKey("UniversityId")]
        public virtual University University { get; set; }

        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(200)]
        public string ResumeFile { get; set; }

        public int? ResumeStatusId { get; set; }
        [ForeignKey("ResumeStatusId")]
        public virtual ResumeStatus ResumeStatus { get; set; }

        public string Skills { get; set; }

        public bool ShowInList { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }

        public int? ConsultantId { get; set; }
        [ForeignKey("ConsultantId")]
        public virtual Consultant Consultant { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
