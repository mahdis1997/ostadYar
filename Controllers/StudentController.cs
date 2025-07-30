using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OstadYarProject.Models;

namespace OstadYarProject.Controllers
{
    public class StudentController : Controller
    {
        private DataBaseContext _db;

        public StudentController()
        {
            _db = new DataBaseContext();
        }
        // GET: Student
        public ActionResult StudentList()
        {
            var result = _db.Student;
            return View(result);
        }

        public ActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("CreateStudent");
            var user =
              _db.Student.Where(
                  m => m.Users.UserName == student.Users.UserName);
            if (user.Any())
            {
                ModelState.AddModelError("ErrorMessage", "نام کاربری جدید را انتخاب کنید، این نام قبلا در سیستم ثبت شده است");
                return View(student);
            }
            _db.Student.Add(student);
            _db.SaveChanges();
            return RedirectToAction("StudentList");
        }

        public ActionResult UpdateStudent(int id)
        {
            var result = _db.Student.Find(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult UpdateStudent(Student student)
        {
            if (!ModelState.IsValid) return RedirectToAction("UpdateStudent");
            var result = _db.Student.Find(student.Id);
            var user =
              _db.Student.Where(
                  m => m.Users.UserName == student.Users.UserName);
            if (user.Any())
            {
                ModelState.AddModelError("ErrorMessage", "نام کاربری جدید را انتخاب کنید، این نام قبلا در سیستم ثبت شده است");
                return View(student);
            }
            result.Name = student.Name;
            result.Family = student.Family;
            result.Email = student.Email;
            result.IdStudent = student.IdStudent;
            result.Users.UserName = student.Users.UserName;
            result.Users.Password = student.Users.Password;
            _db.SaveChanges();
            return RedirectToAction("StudentList");
        }

        public ActionResult DeleteStudent(int id)
        {
            var result = _db.Student.Find(id);
            if (result == null) RedirectToAction("StudentList");
            _db.Student.Remove(result);
            var user = _db.User.Find(result.UserId);
            _db.User.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("StudentList");
        }

        #region student

        public ActionResult DetailInfoStudent()
        {
            var user = Session["Login"] as User;
            var result = _db.Student.SingleOrDefault(m => m.Users.UserName == user.UserName && m.Users.Password == user.Password);
            return View(result);
        }
        [HttpPost]
        public ActionResult DetailInfoStudent(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            var result = _db.Student.Find(student.Id);
            var users =
              _db.Student.Where(
                  m => m.Users.UserName == student.Users.UserName);
            if (users.Any())
            {
                ModelState.AddModelError("Error", "نام کاربری جدید را انتخاب کنید، این نام قبلا در سیستم ثبت شده است");
                return View(student);
            }
            result.Name = student.Name;
            result.Family = student.Family;
            result.Email = student.Email;
            result.IdStudent = student.IdStudent;
            var user = Session["Login"] as User;
            result.Users.Password = user.Password;
            result.Users.UserName = user.UserName;
            _db.SaveChanges();
            ModelState.AddModelError("ErrorMessage", "اطلاعات با موفقیت ثبت شد.");
            return View(result);
        }

        #endregion
    }
}