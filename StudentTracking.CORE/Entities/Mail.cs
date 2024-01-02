using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracking.CORE.Entities
{
    public class Mail
    {
        public int ID { get; set; }
        public string SenderEmail { get; set; }
        public string MailSubject { get; set; }
        public string MailContent { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime? SendDate { get; set; }
        public bool? isActive { get; set; }
        public virtual ICollection<MailRecepient> Recipients { get; set; }
    }
}
