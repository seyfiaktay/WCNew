using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingChecklistNew.Models
{
    public class ChecklistImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public virtual Checklist CheckList { get; set; }
        public int CheckListId { get; set; }
    }
}