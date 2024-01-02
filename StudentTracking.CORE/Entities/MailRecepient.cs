using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracking.CORE.Entities
{
    public class MailRecepient
    {
        public int ID { get; set; }
        public int? MailID { get; set; }
        public string RecipientEmail { get; set; }
        public bool? RecipientType { get; set; } // TO: true, CC: false
        public bool? isActive { get; set; }
        public virtual Mail Mail { get; set; }
    }
}
