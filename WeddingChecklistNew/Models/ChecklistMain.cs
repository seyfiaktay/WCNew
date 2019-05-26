using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingChecklistNew.Models
{
    public class ChecklistMain : clsBase
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("ListName")]
        public string Name { get; set; }
        [DisplayName("Due Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public IEnumerable<Checklist> checklists { get; set; }
    }
}