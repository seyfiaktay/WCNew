using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace WeddingChecklistNew.Models
{
    public class CheckListModel
    {
        public CheckListModel()
        {
            Files = new List<HttpPostedFileBase>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
        public Int16 Priority { get; set; }
        public virtual ChecklistMain CheckListMain { get; set; }
        public int ChecklistMainId { get; set; }
        [DisplayName("Upload File")]
        public string ImagePath { get; set; }
        public List<HttpPostedFileBase> Files { get; set; }
    }
}