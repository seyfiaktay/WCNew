using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeddingChecklistNew.Models.Domain;

namespace WeddingChecklistNew.Models
{
    public class WidgetModel
    {
        public List<RecentlyChecklist> Checklists { get; set; }
        public List<RecentlyChecklist> ChecklistMain { get; set; }
    }
}