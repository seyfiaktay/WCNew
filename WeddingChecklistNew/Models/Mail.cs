using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingChecklistNew.Models
{
    public class Mail
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Attachment { get; set; }
    }
}