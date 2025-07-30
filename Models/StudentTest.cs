using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OstadYarProject.Models
{
    [Table("StudentTest")]
    public class StudentTest
    {
        [Display(Name = "شماره ردیف")]
        public int Id { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime DateTimeStart { get; set; }
        [Display(Name = "نمره دانشجو")]
        public int TotalScore { get; set; }

        public virtual  Student Students { get; set; }
        public virtual  Test Test { get; set; }

    }
}