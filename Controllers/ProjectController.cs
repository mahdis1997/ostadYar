using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OstadYarProject.Models;

namespace OstadYarProject.Controllers
{
    public class ProjectController : Controller
    {
        private DataBaseContext _db;
        public ProjectController()
        {
            _db = new DataBaseContext();
        }
        public ActionResult ProjectList()
        {
            var result = _db.Project;
            return View(result);
        }

        public ActionResult CreateProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProject(Project project)
        {
            if (!ModelState.IsValid) return CreateProject(project);
            _db.Project.Add(project);
            _db.SaveChanges();
            return RedirectToAction("ProjectList");
        }
        public ActionResult UpdateProject(int id)
        {
            var result = _db.Project.Find(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult UpdateProject(Project project)
        {
            if (!ModelState.IsValid) return View(project);
            var result = _db.Project.Find(project.Id);
            result.Title = project.Title;
            result.DateTimeEnd = project.DateTimeEnd;
            result.DateTimeStart = project.DateTimeStart;
            result.Description = project.Description;
            result.Score = project.Score;
            _db.SaveChanges();
            return RedirectToAction("ProjectList");
        }
        public ActionResult DeleteProject(int id)
        {
            var result = _db.Project.Find(id);
            if (result == null) return RedirectToAction("ProjectList");
            _db.Project.Remove(result);
            _db.SaveChanges();
            return RedirectToAction("ProjectList");
        }

        public ActionResult ListProjectStudent()
        {
            var result = _db.ProjectStudent;
            return View(result);
        }

        public ActionResult ViewDetailsProjectStudent(int id)
        {
            var result = _db.ProjectStudent.Find(id);
            return View(result);
        }

        public ActionResult DownloadFile(int id)
        {
            var result = _db.ProjectStudent.Where(m => m.Id == id);
            var fileData = (byte[])result.First().File.ToArray();
            return File(fileData, "text",result.First().FileName);
        }

        [HttpPost]
        public ActionResult ViewDetailsProjectStudent(ProjectStudent projectStudent)
        {
            var result = _db.ProjectStudent.SingleOrDefault(m => m.Id == projectStudent.Id);
            result.Score = projectStudent.Score;
            _db.SaveChanges();
            return RedirectToAction("ListProjectStudent");
        }

        #region Student

        public ActionResult ProjectListForStudent()
        {
            
            var result = _db.Project;
            return View(result);
        }

        public ActionResult DetailProject(int id)
        {
            var user = Session["Login"] as User;
            if (user == null) return RedirectToAction("Login", "Home");
            var student =
                           _db.Student.SingleOrDefault(m => m.Users.UserName == user.UserName && m.Users.Id == user.Id);
            var result = _db.Project.Find(id);

            if (result.DateTimeEnd <= DateTime.Now)
            {
                ModelState.AddModelError("Error", "!مهلت برای ارسال پروژه به اتمام رسیده است");
                return View("ProjectListForStudent", _db.Project); 
            }
            if (_db.ProjectStudent.Any(m => m.Students.Id == student.Id && m.Project.Id == id))
            {
                ModelState.AddModelError("Error","شما قبلا پروژه خود را ارسال کردید");
                return View("ProjectListForStudent", _db.Project); 
            }

            var projectstudent = new ProjectStudent
            {
                Project = result
            };
            return View(projectstudent);
        }

        [HttpPost]
        public ActionResult DetailProject(ProjectStudent projectStudent, HttpPostedFileBase upload)
        {
            var user = Session["Login"] as User;
            var student =
                 _db.Student.SingleOrDefault(m => m.Users.UserName == user.UserName && m.Users.Id == user.Id);
            if (upload == null) return View(projectStudent);
            var project = _db.Project.SingleOrDefault(m => m.Id == projectStudent.Project.Id);
            projectStudent.File = ConvertToBytes(upload);
            projectStudent.FileName = upload.FileName;
            projectStudent.Project = project;
            projectStudent.Students = student;
            projectStudent.Score = -1;
            if (!ModelState.IsValid)
                return View(projectStudent);
            _db.ProjectStudent.Add(projectStudent);
            _db.SaveChanges();
            return RedirectToAction("ProjectListForStudent");
        }

        #endregion
        public static byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}