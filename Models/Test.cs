using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OstadYarProject.Models
{
    [Table("Test")]
    public class Test
    {
        [Display(Name = "شماره ردیف")]
      public int Id { get; set; }
        [Display(Name = "تاریخ و ساعت شروع آزمون")]
        public DateTime DateTimeStart { get; set; }
        [Display(Name = "تاریخ و ساعت اتمام آزمون")]
        public DateTime DateTimeEnd { get; set; }
        [Display(Name = "عنوان آزمون")]
        public string Title { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "مدت زمان آزمون")]
        public double Duration { get; set; }
    }
}