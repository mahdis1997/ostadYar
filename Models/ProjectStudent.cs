using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OstadYarProject.Models
{
    [Table("ProjectStudent")]
    public class ProjectStudent
    {

        [Display(Name = "شماره ردیف")]
        public int Id { get; set; }
        [AllowHtml,Display(Name = "فایل ارسالی")]
        public byte[] File { get; set; }
        [Required(ErrorMessage = "متن مورد نظر خود را وارد کنید")]
        [AllowHtml,Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "اسم فایل")]
        public string FileName { get; set; }
        [Display(Name = "نمره دانشجو")]
        public int Score { get; set; }
        public virtual Student Students { get; set; }
        public virtual Project Project { get; set; }
    }
}