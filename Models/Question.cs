using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace OstadYarProject.Models
{
    [Table("Question")]
    public class Question
    {
        [Display(Name = "شماره ردیف")]
        public int Id { get; set; }
        [Required(ErrorMessage = "شماره ردیف سوال را وارد نمایید ."),Display(Name = "شماره ردیف سوال")]
        public int DisplayOrder { get; set; }
        [Required(ErrorMessage = "عنوان سوال را وارد نمایید ."), Display(Name = "عنوان سوال")]
        public string QuestionTitle { get; set; }
        [Required(ErrorMessage = "گزینه اول سوال را وارد نمایید ."),Display(Name = "گزینه اول")]
        public string Option1 { get; set; }
        [Required(ErrorMessage = "گزینه دوم سوال را وارد نمایید ."), Display(Name = "گزینه دوم")]
        public string Option2 { get; set; }
        [Required(ErrorMessage = "گزینه سوم سوال را وارد نمایید ."), Display(Name = "گزینه سوم")]
        public string Option3 { get; set; }
        [Required(ErrorMessage = "گزینه چهارم سوال را وارد نمایید ."), Display(Name = "گزینه چهارم")]
        public string Option4 { get; set; }
        [Required(ErrorMessage = "جواب سوال را وارد نمایید ."), Display(Name = "پاسخ")]
        public int Answer { get; set; }
        [Required(ErrorMessage = "بارم سوال را وارد نمایید ."), Display(Name = "نمره")]
        public int Score { get; set; }

        public virtual Test Test { get; set; }

    }

   
}