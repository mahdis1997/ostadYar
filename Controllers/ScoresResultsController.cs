using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OstadYarProject.Models;

namespace OstadYarProject.Controllers
{
    public class ScoresResultsController : Controller
    {
        private DataBaseContext _db;

        public ScoresResultsController()
        {
            _db=new DataBaseContext();
        }
        public ActionResult ListScoresResult()
        {
            var user = Session["Login"] as User;
            var student =
                _db.Student.SingleOrDefault(m => m.Users.UserName == user.UserName && m.Users.Password == user.Password);
            var result = _db.StudentTest.Where(m => m.Students.Id == student.Id).ToList();
            return View(result);
        }

        public ActionResult ListScoreProjectResult()
        {
            var user = Session["Login"] as User;
            var student =
                _db.Student.SingleOrDefault(m => m.Users.UserName == user.UserName && m.Users.Password == user.Password);
            var result = _db.ProjectStudent.Where(m => m.Score != -1 && m.Students.Id == student.Id).ToList();
            return View(result);
        }
    }
}