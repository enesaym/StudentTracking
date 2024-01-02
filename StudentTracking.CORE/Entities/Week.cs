using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracking.CORE.Entities
{
    public class Week
    {
        public int ID { get; set; }
        public int? ClassID { get; set; }
        public string WeekName { get; set; }
        public virtual Class Class { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
