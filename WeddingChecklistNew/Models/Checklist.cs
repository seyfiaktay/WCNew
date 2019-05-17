using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingChecklistNew.Models
{
    public class Checklist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
        public Int16 Priority { get; set; }
        public IEnumerable<ChecklistImage> CheckListImage { get; set; }
        public virtual ChecklistMain CheckListMain { get; set; }
        public int ChecklistMainId { get; set; }
    }
}