using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using StudentTracking.BLL.Manager;
using StudentTracking.CORE.EmailService;
using StudentTracking.VM.Class;
using StudentTracking.VM.Report;
using StudentTracking.VM.Student;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentTracking.UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportManager _reportManager;
        private readonly StudentManager _studentManager;
        private readonly ClassManager _classManager;
        private readonly MailSender _mailSender;

        public ReportController(ReportManager reportManager, StudentManager studentManager, MailSender mailSender, ClassManager classManager)
        {
            _reportManager = reportManager;
            _studentManager = studentManager;
            _mailSender = mailSender;
            _classManager = classManager;
        }

        // Ekleme sayfası
        [HttpGet]
        public IActionResult AddReport()
        {
            ViewBag.StudentList = new SelectList(_studentManager.GetAllJustFullName().Data, "ID", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult AddReport(ReportInsertVM reportInsertVM)
        {
            _reportManager.Add(reportInsertVM);

            return RedirectToAction("SelectReport");
        }



        // ID gelicek ona göre sayfadaki textler dolacak
        [HttpGet]
        public IActionResult UpdateReport(int ID)
        {
            ReportSelectVM myClass = _reportManager.GetByID(ID).Data;

            var student = _studentManager.GetByID(myClass.StudentID);

            ReportUpdateVM myUpdateClass = new ReportUpdateVM
            {
                ID = myClass.ID,
                StudentID = myClass.StudentID,
                FirstName = student.Data.FirstName,
                LastName = student.Data.LastName,
                Description = myClass.Description,
                Score = myClass.Score,
                Date = myClass.Date,
                WeekOfYear = myClass.WeekOfYear
            };
            
            ViewBag.StudentList = new SelectList(_studentManager.GetAllJustFullName().Data, "ID", "FullName", myUpdateClass.StudentID);


            return View(myUpdateClass);
        }

        [HttpPost]
        public IActionResult UpdateReport(ReportUpdateVM reportUpdateVM)
        {
            _reportManager.Update(reportUpdateVM);

            return RedirectToAction("SelectReport");
        }


        // Listeleme sayfası : update ve delete butonları tablonun en sağına koyulabilir.
        [HttpGet]
        public IActionResult SelectReport()
        {


            //var reports = _reportManager.GetAll().Data;

            //foreach (var report in reports)
            //{

            //    var student = _studentManager.GetByID(report.StudentID);
            //    report.FirstName = student?.Data.FirstName;
            //    report.LastName = student?.Data.LastName;
            //}


            return View(_reportManager.GetAllWithStudentID().Data);
        }


        public IActionResult DeleteReport(int ID)
        {
            _reportManager.SoftDelete(ID);
            return RedirectToAction("SelectReport");
        }

        [HttpGet]
        public IActionResult SelectSendReport()
        {
            ViewBag.classes = _classManager.GetAll().Data;
            
            return View();
        }

        [HttpPost]
        public IActionResult SendReport(string konuBasligi, string mesajIcerigi, string sinifSecimi, int haftaSecimi)
        {
            var selectedClass = JsonSerializer.Deserialize<ClassNewVM>(sinifSecimi);
            var students = _studentManager.GetStudentWithDetailsReport(selectedClass.id,haftaSecimi).Data;
            string raporName = ConvertExcel(students, haftaSecimi, selectedClass.startedDate,selectedClass.endDate);

            _mailSender.SendEmail("furkangokirmak34@gmail.com", konuBasligi, mesajIcerigi, raporName);
            return View();
        }

        private string ConvertExcel(List<StudentSelectVM> students, int hafta,DateTime start,DateTime end) 
        {
            /*
             *  Eksikler: 
             *  - start ve end date program başlangıç ve bitiş olarak geliyor. Haftanın başlangıç ve bitişi gelmeli.
             *  - Eğitmen görüşü genel olarak bir bölüm belirtilmiş oyle birşey tutmuyoruz.
             *  
             *  ---
             *  Sadece report tablosunda bulunan haftalık genel görüşmü gerekli,  : 
             *  kişinin sorulara verdiği cevaplar, yaptığı projeler nasıl eklenecek excele 
             *  
             */

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelPackage = new ExcelPackage();
            var worksheet = excelPackage.Workbook.Worksheets.Add("People");

            worksheet.Column(3).Width = 20;
            worksheet.Column(4).Width = 15;
            worksheet.Column(5).Width = 50;


            for (int i = 1; i <= students.Count; i++)
            {
                worksheet.Cells[4+i, 2].Value = i;
                worksheet.Cells[4+i, 3].Value = students[i-1].FirstName + " " + students[i-1].LastName;
                worksheet.Cells[4 + i, 4].Value = students[i-1].Report.Select(x => x.Score).FirstOrDefault();
                worksheet.Cells[4 + i, 5].Value = students[i-1].Report.Select(x => x.Description).FirstOrDefault();
    //            for (int j; j = 0; j++ ; var item in students[i - 1].StudentExam)
				//{
    //                worksheet.Cells[4 + i, 6+j].Value = item.Score;
    //            }
               
                worksheet.Cells[4 + i, 5].Value = students[i - 1].Report.Select(x => x.Description).FirstOrDefault();
            }
       
            worksheet.Cells["D3:E3"].Merge = true;
            worksheet.Cells["D3:E3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["D3"].Value = FormatDateRange(start, end);
            worksheet.Cells["C4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["C4"].Value = "Ad Soyad";
            worksheet.Cells["D4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["D4"].Value = hafta+".Hafta";
            worksheet.Cells["E4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells["E4"].Value = "Açıklama";

            worksheet.Cells["D" + (students.Count + 6)].Value = "Eğitmen Görüşü:";

            using (var range = worksheet.Cells["D"+ (students.Count + 6) + ":E" + (students.Count + 6)])
            {
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }

            using (var range = worksheet.Cells["B3:E"+(students.Count+4)])
            {
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }
            Guid raporId = Guid.NewGuid();
            string raporName = "Rapor_" + raporId + ".xlsx";
            var fileInfo = new FileInfo(raporName);
            excelPackage.SaveAs(fileInfo);

            return raporName;
        }

        private string FormatDateRange(DateTime startDate, DateTime endDate)
        {
            string formattedDateRange;

            if (startDate.Month == endDate.Month)
            {
                // Aynı ay içindeyse sadece günleri göster
                formattedDateRange = $"{startDate.Day} - {endDate.Day} {GetMonthName(startDate.Month)}";
            }
            else
            {
                // Farklı aylarda ise hem ay hem günleri göster
                formattedDateRange = $"{startDate.Day} {GetMonthName(startDate.Month)} - {endDate.Day} {GetMonthName(endDate.Month)}";
            }

            return formattedDateRange;
        }

        private string GetMonthName(int month)
        {
            // Ay ismini almak için kültüre bağlı olmayan bir şekilde tarih formatı kullanabilirsiniz
            return new DateTime(1, month, 1).ToString("MMMM");
        }

    }
}