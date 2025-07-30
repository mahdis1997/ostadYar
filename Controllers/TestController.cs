using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using OstadYarProject.Models;

namespace OstadYarProject.Controllers
{
    public class TestController : Controller
    {
        private DataBaseContext _db;

        public TestController()
        {
            _db = new DataBaseContext();
        }

        public ActionResult ListTest()
        {
            var result = _db.Test;
            return View(result);
        }
        public ActionResult CreateTest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTest(Test test)
        {
            if (!ModelState.IsValid)
                return View("CreateTest", test);
            _db.Test.Add(test);
            _db.SaveChanges();
            return RedirectToAction("ListTest");
        }
        public ActionResult UpdateTest(int id)
        {
            var result = _db.Test.Find(id);
            if (result == null) return RedirectToAction("ListTest");
            return View(result);
        }
        [HttpPost]
        public ActionResult UpdateTest(Test test)
        {
            if (!ModelState.IsValid) return View(test);
            var result = _db.Test.Find(test.Id);
            result.Title = test.Title;
            result.Duration = test.Duration;
            result.DateTimeStart = test.DateTimeStart;
            result.DateTimeEnd = test.DateTimeEnd;
            _db.SaveChanges();
            return RedirectToAction("ListTest");
        }
        public ActionResult ViewDetails(int id)
        {
            var result = _db.Question.Where(m => m.Test.Id == id).OrderBy(n => n.DisplayOrder).ToList();
            ViewBag.TestId = id;
            var question = _db.Question.Where(b => b.Test.Id == id);
            var total = Enumerable.Sum(question, item => item.Score);
            ViewBag.TotalScore = total;
            return View("ViewDetails", result);
        }
        public ActionResult DeleteTest(int id)
        {
            var result = _db.Test.Find(id);
            if (result == null) return RedirectToAction("ListTest", false);
            _db.Test.Remove(result);
            _db.SaveChanges();
            return RedirectToAction("ListTest");
        }

        public ActionResult TestListForAdmin()
        {
            var result = _db.StudentTest;
            return View(result);
        }

        #region Student

        public ActionResult TestListForStudent()
        {
            var result = _db.Test;
            return View(result);
        }

        public ActionResult Exam(int id)
        {
            var user = Session["Login"] as User;
            if (user == null) return RedirectToAction("Login", "Home");
            var test = _db.Test.Find(id);
            var student =
                _db.Student.SingleOrDefault(m => m.Users.UserName == user.UserName && m.Users.Password == user.Password);
            var studenttestt =
                _db.StudentTest.SingleOrDefault(m => m.Students.Users.Id == user.Id && m.Test.Id == id && m.TotalScore == -1);
            if (test.DateTimeEnd <= DateTime.Now)
            {
                ModelState.AddModelError("Error", "!مهلت شرکت در امتحان به اتمام رسیده است");
                return View("TestListForStudent", _db.Test); 
            }
            if(_db.StudentTest.Any(m => m.Students.Users.Id==user.Id && m.Test.Id==id && m.TotalScore!=-1 ))
            {
               ModelState.AddModelError("Error","!شما قبلا درآزمون شرکت کردید ");
                return View("TestListForStudent" ,_db.Test);
            }
            var studenttest=new StudentTest
            {
                DateTimeStart = DateTime.Now,Students = student,Test =test ,TotalScore = -1
            };

            var question = _db.Question.Where(m => m.Test.Id == id);
            if (!question.Any())
            {
                ModelState.AddModelError("Error", "برای این آزمون سوالی طرح نشده است و غیر معتبر می باشد");
                return View("TestListForStudent", _db.Test);
            }

            if (studenttestt != null)
                return View(question);

            _db.StudentTest.Add(studenttest);
            _db.SaveChanges();

            return View(question);
        }

        public JsonResult GetAnswer(string id)
        {
            var user = Session["Login"] as User;
            var student =
                 _db.Student.SingleOrDefault(m => m.Users.UserName == user.UserName && m.Users.Password == user.Password);
            var question = _db.Question.AsEnumerable().Where(m => m.Test.Id == int.Parse(id)).ToList();
            var answer = question.Select(item => _db.Answer.SingleOrDefault(m => m.Questions.Id == item.Id && m.Students.Id == student.Id)).ToList();
            return Json(answer, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddAnswer(string idRadioButton)
        {
            var idquestion = idRadioButton.Split().Last();
            var question = _db.Question.Find(int.Parse(idquestion));
            char[] splitchar = { 'o' };
            var answerstudent = idRadioButton.Split().First().Split(splitchar).Last();
            var user = Session["Login"] as User;
            var student =
                _db.Student.SingleOrDefault(m => m.Users.UserName == user.UserName && m.Users.Password == user.Password);
            var answer = _db.Answer.SingleOrDefault(m => m.Questions.Id == question.Id && m.Students.Id == student.Id);
            if (answer == null)
            {
                var answers = new Answer
                {
                    Questions = question,
                    StudentAnswer = int.Parse(answerstudent),
                    Students = student
                };
                _db.Answer.Add(answers);
            }
            else
                answer.StudentAnswer = int.Parse(answerstudent);

            _db.SaveChanges();
            return Json("!جواب شما ثبت شد", JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetTest(int id)
        {
            var result = _db.Test.Find(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult SaveExam(string  id)
        {
            var user = Session["Login"] as User;
            var student =
                 _db.Student.SingleOrDefault(m => m.Users.UserName == user.UserName && m.Users.Password == user.Password);
            var question = _db.Question.AsEnumerable().Where(m => m.Test.Id == int.Parse(id)).ToList();
            var answer = question.Select(item => _db.Answer.SingleOrDefault(m => m.Questions.Id == item.Id && m.Students.Id == student.Id)).ToList();
            var result = answer.Select(item => _db.Question.AsEnumerable().SingleOrDefault(m =>
                                m.Answer == item.StudentAnswer &&
                                m.Id == item.Questions.Id &&
                                m.Test.Id == int.Parse(id))).Where(a => a != null).ToList().Sum(m=>m.Score);
           
            var studenttest =
                _db.StudentTest.AsEnumerable().SingleOrDefault(m => m.Test.Id == int.Parse(id) && m.Students.Id == student.Id);
            studenttest.TotalScore = result;
            studenttest.DateTimeStart=DateTime.Now;
            _db.SaveChanges();
            return RedirectToAction("TestListForStudent" , "Test");
        }
        
        #endregion
    }
}