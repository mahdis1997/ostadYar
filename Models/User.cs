using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OstadYarProject.Models
{
    [Table("User")]
    public class User
    {
        [Display(Name = "شماره ردیف")]
        public int Id { get; set; }
        [Required(ErrorMessage = "نام کاربری را وارد کنید ."),Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "رمزعبور را وارد کنید ."), Display(Name = "رمز عبور")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}