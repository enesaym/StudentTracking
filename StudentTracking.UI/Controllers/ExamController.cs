using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentTracking.BLL.Manager;
using StudentTracking.CORE.Entities;
using StudentTracking.VM.Exam;
using StudentTracking.VM.Student;
using StudentTracking.VM.StudentExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamTracking.UI.Controllers
{
    public class ExamController : Controller
    {
        private readonly ExamManager _examManager;
        private readonly StatusManager _statusManager;
        private readonly ClassManager _classManager;
        private readonly StudentManager _studentManager;

        public ExamController(ExamManager examManager, StatusManager statusManager, ClassManager classManager, StudentManager studentManager)
        {
            _examManager = examManager;
            _statusManager = statusManager;
            _classManager = classManager;
            _studentManager = studentManager;
        }

        // Ekleme sayfası
        [HttpGet]
        public IActionResult AddExam()
        {
            ViewBag.StatusList = new SelectList(_statusManager.GetAll().Data.Where(a => a.ID > 200 && a.ID < 300), "ID", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddExam(ExamInsertVM examInsertVM)
        {
            _examManager.Add(examInsertVM);
            return RedirectToAction("SelectExam");
        }



        // ID gelicek ona göre sayfadaki textler dolacak
        [HttpGet]
        public IActionResult UpdateExam(int ID)
        {
            ExamSelectVM examSelectVM = _examManager.GetByID(ID).Data;

            var status = _statusManager.GetByID(examSelectVM.StatusID.Value);
            ExamUpdateVM ex = new ExamUpdateVM
            {
                Name = examSelectVM.Name,
                ID = examSelectVM.ID,
                Body = examSelectVM.Body,
                Date = examSelectVM.Date.Value,
                StatusName = status.Data.Name,

            };
            ViewBag.StatusList = new SelectList(_statusManager.GetAll().Data.Where(a => a.ID > 200 && a.ID < 300), "ID", "Name", ex.StatusID);

            return View(ex);
        }

        [HttpPost]
        public IActionResult UpdateExam(ExamUpdateVM examUpdateVM)
        {
            _examManager.Update(examUpdateVM);
            return RedirectToAction("SelectExam");

        }



       [HttpGet]
        public IActionResult SelectExam()
        {
            List<ExamSelectVM> exams = _examManager.GetAllWithStatus().Data;

            return View(exams);
        }


        public IActionResult DeleteExam(int ID)
        {
            _examManager.SoftDelete(ID);
            return RedirectToAction("SelectExam");
        }

        [HttpGet]
        public IActionResult AddExamGrades()
        {
            var classes = _classManager.GetAll().Data;
            classes.ForEach(x => 
            {
                x.Exam = _examManager.GetExamsByClassId(x.ID).Data;
            });

            return View(classes);
        }

        [HttpPost]
        public IActionResult AddExamGrades(int classId, int examId)
        {
            TempData["ClassID"] = classId;
            HttpContext.Session.SetString("ExamID", examId.ToString());

            return RedirectToAction("AddExamGradeDetails");
        }

        [HttpGet]
        public IActionResult AddExamGradeDetails()
        {
            var classId = (int)TempData["ClassID"];
            var examId = Convert.ToInt32(HttpContext.Session.GetString("ExamID"));
            

            var students = _studentManager.GetStudentExamsByClassId(classId).Data;

            students.ForEach(x => 
            {
                var studentExam = x.StudentExam.FirstOrDefault(x=> x.ExamID == examId);
                if (studentExam != null)
                {
                    x.ClassID = studentExam.Score.Value;
                    x.StatusName = studentExam.Description;
                    x.StatusID = 1;
                }
                else
                {
                    x.ClassID = 0;
                    x.StatusName = "";
                    x.StatusID = 0;
                }
            });

            return View(students);
        }

        [HttpPost]
        public IActionResult SaveExamGrades(List<StudentSelectVM> model)
        {
            var examId = Convert.ToInt32(HttpContext.Session.GetString("ExamID"));
            if (ModelState.IsValid)
            {
                model.ForEach(x => 
                {
                    var studentExam = new StudentExamSelectVM { StudentID = x.ID, ExamID = examId, Score = x.ClassID, Description = x.StatusName };

                    if (x.StatusID == 1)
                    {                        
                        _examManager.UpdateExamGrades(studentExam);
                    }
                    else
                    {
                        _examManager.AddExamGrades(studentExam);
                    }                                  
                });              
            }

            return RedirectToAction("AddExamGrades");
        }

        [HttpGet]
        public IActionResult GetExamsByClass(int classId)
        {
            var exams = _examManager.GetExamsByClassId(classId).Data;

            return Json(exams); 
        }
    }
}