using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OstadYarProject.Models;

namespace OstadYarProject.Controllers
{
    public class QuestionController : Controller
    {
        private DataBaseContext _db;

        public QuestionController()
        {
            _db=new DataBaseContext();
        }
        // GET: Question
        public ActionResult CreateQuestion(int id)
        {
            var test = _db.Test.Find(id);
            var question=new Question{Test = test};
            return View(question);
        }
        [HttpPost]
        public ActionResult CreateQuestion(Question question)
        {
            if (!ModelState.IsValid)
                return CreateQuestion(question.Test.Id);
            
            var test = _db.Test.Find(question.Test.Id);
            question.Test = test;
            if (_db.Question.Count(m => m.Test.Id == question.Test.Id) > 10)
            {
                ModelState.AddModelError("ErrorMessage","تعداد سوالات شما بیش از 10 عدد میباشد و شما قادر به طرح سوال نیستید! ");
                return CreateQuestion(test.Id);

            }
            _db.Question.Add(question);
            _db.SaveChanges();
            var result=new Question{Test = test};
            ModelState.AddModelError("Erorr","سوال با موفقیت ثبت شد");
            return View("CreateQuestion",result);
        }

        public ActionResult UpdateQuestion(int id)
        {
            var result = _db.Question.Find(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult UpdateQuestion(Question question)
        {
            if (!ModelState.IsValid)
                return CreateQuestion(question.Test.Id);
            var result = _db.Question.Find(question.Id);
            result.Answer = question.Answer;
            result.QuestionTitle = question.QuestionTitle;
            result.Option1 = question.Option1;
            result.Option2 = question.Option2;
            result.Option3 = question.Option3;
            result.Option4 = question.Option4;
            result.DisplayOrder = question.DisplayOrder;
            result.Score = question.Score;
            _db.SaveChanges();

            return View(result);
        }

        public ActionResult DeleteQuestion(int id)
        {
            var result = _db.Question.Find(id);
            _db.Question.Remove(result);
            _db.SaveChanges();
            return RedirectToAction("ListTest", "Test");
        }
    }

}