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
        public Resume()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            Approved = false;
            ShowInList = true;
        }

        public int Id { get; set; }

        [StringLength(200)]
        [Display(Name = "Resim")]
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Kulanıcı adı gereklidir.")]
        [StringLength(200)]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [StringLength(200)]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        [StringLength(200)]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Cinsiyet alanı gereklidir.")]
        [Display(Name = "Cinsiyet")]
        [EnumDataType(typeof(Models.Gender))]
        public Gender Gender { get; set; }
        [Display(Name = "Meslek")]
        public int? ProfessionId { get; set; }
        [Display(Name = "Meslek")]
        [ForeignKey("ProfessionId")]
        public virtual Profession Profession { get; set; }
        [Display(Name = "Eğitim Seviyesi")]
        public int? EducationLevelId { get; set; }
   
        [ForeignKey("EducationLevelId")]
        [Display(Name = "Eğitim Seviyesi")]
        public virtual EducationLevel EducationLevel { get; set; }

        [Display(Name = "Üniversite")]
        public int? UniversityId { get; set; }
        [ForeignKey("UniversityId")]
        [Display(Name = "Üniversite")]
        public virtual University University { get; set; }
        [Display(Name = "Bölüm")]
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        [Display(Name = "Bölüm")]
        public virtual Department Department { get; set; }
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = "dd.MM.yyyy", NullDisplayText = "")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Özgeçmiş Dosyası")]
        [StringLength(200)]
        [DataType(DataType.Upload)]
        public string ResumeFile { get; set; }

        [Display(Name = "Durum")]
        public int? ResumeStatusId { get; set; }
        [ForeignKey("ResumeStatusId")]
        [Display(Name = "Durum")]
        public virtual ResumeStatus ResumeStatus { get; set; }

        [Display(Name = "Yetenekler")]
        [DataType(DataType.MultilineText)]
        public string Skills { get; set; }

        [Display(Name = "Listede Göster")]
        public bool ShowInList { get; set; }

       
        [StringLength(200)]
        [Display(Name = "Şehir")]
        public string City { get; set; }

      
        
        [StringLength(200)]
        [Display(Name = "İlçe")]
        public string District { get; set; }

        [Display(Name = "Eğitmen")]
        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        [Display(Name = "Eğitmen")]
        public virtual Teacher Teacher { get; set; }
        [Display(Name = "Danışman")]
        public int? ConsultantId { get; set; }
        [ForeignKey("ConsultantId")]
        [Display(Name = "Danışman")]
        public virtual Consultant Consultant { get; set; }
        [Display(Name = "Onaylandı")]
        public bool Approved { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Güncellenme Tarihi")]
        public DateTime UpdateDate { get; set; }

    }
}
