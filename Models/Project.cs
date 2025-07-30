using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OstadYarProject.Models
{
    [Table("Project")]
    public class Project
    {
        [Display(Name = "شماره ردیف")]
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان پروژه را وارد کنید ."),Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Required(ErrorMessage = "تاریخ شروع پروژه را وارد کنید ."),Display(Name = "تاریخ و ساعت شروع تمرین")]
        public DateTime DateTimeStart { get; set; }
        [Required(ErrorMessage = "تاریخ پایان پروژه را وارد کنید ."), Display(Name = "تاریخ و ساعت اتمام تمرین")]
        public DateTime DateTimeEnd { get; set; }
        [Required(ErrorMessage = "نمره پروژه را وارد کنید ."),Display(Name = "نمره تمرین")]
        public double Score { get; set; }
    }
}