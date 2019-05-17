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
        [DisplayName("ListName")]
        [Required(ErrorMessage = "List Name is required.")]
        [MaxLength(250, ErrorMessage = "List Name cannot be longer than 250 characters.")]
        public string Name { get; set; }
        [DisplayName("Due Date")]

        [Required(ErrorMessage = "Enter due date.")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        public IEnumerable<Checklist> checklists { get; set; }
    }
}