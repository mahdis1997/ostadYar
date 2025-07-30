using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OstadYarProject.Models
{
    [Table("Answer")]
    public class Answer
    {
        [Display(Name = "شماره ردیف")]

        public int Id { get; set; }
        [Display(Name = "جواب دانشجو")]
        public int StudentAnswer { get; set; }

        public virtual Student Students { get; set; }
        public virtual Question Questions { get; set; }

    }
}