using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentTracking.BLL.Manager;
using StudentTracking.VM.Class;
using StudentTracking.VM.Question;
using StudentTracking.VM.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTracking.UI.Controllers
{
    public class FastController : Controller
    {

       
        private readonly ClassManager _classManager;
        private readonly StudentManager _studentManager;
        private readonly QuestionManager _questionManager;
        public FastController(ClassManager classManager, StudentManager studentManager, QuestionManager questionManager)
        {
            _classManager = classManager;
            _studentManager = studentManager;
            _questionManager = questionManager;
        }

        // Sınıf seçme listesi
        [HttpGet]
        public IActionResult FastSelectClass(ClassSelectVM classSelectVM)
        {
            List<ClassSelectVM> classList = _classManager.GetAll().Data;
            return View(classList);
        }

        // Sınıf id si alınıp kartlara öğrenci listesi yollanacak
        [HttpPost]
        public IActionResult FastSelectClass(int SelectedClassId)
        {
            var c = _studentManager.GetStudentWithDetailsReport(SelectedClassId, 2);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30),
            };

            Response.Cookies.Append("ClassIDCookie", SelectedClassId.ToString(), options);


            return RedirectToAction("FastStudentManagement");
        }
        // Kartların gelmesini sağlayacak get işlemi
        [HttpGet]
        public IActionResult FastStudentManagement()
        {
            if (Request.Cookies.TryGetValue("ClassIDCookie", out string classId))
            {
                int selectedClassID;
                if (int.TryParse(classId, out selectedClassID))
                {
                    var a = _studentManager.GetStudentWithDetails(selectedClassID).Data;

                    //List<StudentSelectVM> selectedStudents = _studentManager.GetStudentsByClass(selectedClassID).ToList();
                    //studentların sorularını burda listeye ekliyorum 
                    //selectedStudents.ForEach(x =>
                    //{
                    //    x.StudentQuestions = _questionManager.GetByStudentID(x.ID).ToList();

                    //});

                    return View(a);
                }
            }
            return View();
            //int? selectedClassID = HttpContext.Session.GetInt32("SelectedClassID");
            //List<StudentSelectVM> selectedStudents = _studentManager.GetByClassID(Convert.ToInt32(selectedClassID)).ToList();
        }

        // kartlardaki arka yüzde soru sorunca çalışan method
        [HttpPost]
        public IActionResult FastStudentManagement(string Question, string Description, int StudentId)
        {

            _questionManager.Add(new QuestionInsertVM { StudentID = StudentId, QuestionName = Question, Description = Description, Date = new DateTime(2023,11,29) });

            return Ok();       
        }

        [HttpGet]
        public IActionResult FastStudentGetDetails(int StudentId)
        {
            return ViewComponent("Card", _studentManager.GetStudentWithDetailsByID(StudentId).Data);
        }

    }
}
