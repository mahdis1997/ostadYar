using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using OstadYarProject.Models;

namespace OstadYarProject.Controllers
{
    public class HomeController : Controller
    {
        private DataBaseContext _db;

        public HomeController()
        {
            _db = new DataBaseContext();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult SignOut()
        {
            Session["Login"] = null;
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult SignUp(Student student)
        {
            if (!ModelState.IsValid) return View("SignUp", student);
            var user =
                _db.Student.Where(
                    m => m.Users.UserName == student.Users.UserName);
            if (user.Any())
            {
                ModelState.AddModelError("ErrorMessage","نام کاربری جدید را انتخاب کنید، این نام قبلا در سیستم ثبت شده است");
                return View(student);
            }
            _db.Student.Add(student);
            _db.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (!ModelState.IsValid) return View("Login", user);
            var result = _db.User.SingleOrDefault(m => m.UserName == user.UserName && m.Password == user.Password);
            if (result == null)
            {
                ModelState.AddModelError("ErrorMessage", "نام کاربری یا رمزعبور اشتباه می باشد .");
                return View("Login");
            }
            Session["Login"] = result;
            if (result.IsAdmin)
                return RedirectToAction("Home");

            return RedirectToAction("HomeStudent", "Home");
        }
        #region  student

        public ActionResult HomeStudent()
        {
            if (Session["Login"] != null)
                return View();
            return RedirectToAction("Login");
        }

        #endregion

        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult AboutUss()
        {
            return View();
        }
    }
}