using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracking.VM.Class
{
    public class ClassNewVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public int capacity { get; set; }
        public DateTime startedDate { get; set; }
        public DateTime endDate { get; set; }
        public bool isActive { get; set; }
    }
}
