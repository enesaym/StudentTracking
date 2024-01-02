using StudentTracking.VM.Exam;
using StudentTracking.VM.Question;
using StudentTracking.VM.Report;
using StudentTracking.VM.StudentExam;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracking.VM.Student
{
    public class StudentSelectVM
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int StatusID { get; set; }
        public int ClassID { get; set; }
        public string StatusName { get; set; }
        public string ClassName { get; set; }
        public IEnumerable<QuestionSelectVM> Question { get; set; }
        public virtual ICollection<StudentExamSelectVM> StudentExam { get; set; }
        public virtual ICollection<ReportSelectVM> Report { get; set; }
        public bool isActive { get; set; }
    }
}
