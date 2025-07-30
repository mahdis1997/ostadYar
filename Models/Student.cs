using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OstadYarProject.Models
{
    [Table("Student")]
    public class Student
    {
        [Display(Name = "شماره ردیف")]
        public int Id { get; set; }
        [Required(ErrorMessage = "نام را وارد کنید ."),Display(Name = "نام")]
        public string Name { get; set; }
        [Required(ErrorMessage = "نام خانوادگی را وارد کنید ."), Display(Name = " نام خانوادگی")]
        public string Family { get; set; }
        [Required(ErrorMessage = "شماره دانشجویی را وارد کنید ."), Display(Name = "شماره دانشجویی")]
        public int IdStudent { get; set; }
        [Required(ErrorMessage = "ایمیل را وارد کنید ."), Display(Name = "ایمیل")]
        public string Email { get; set; }

        public virtual User Users { get; set; }
        public int UserId { get; set; }

    }
}