using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingChecklistNew.Models
{
    public class Comment : clsBase
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Comment")]
        public string Text { get; set; }
        [Required]
        public int ChecklistMainId { get; set; }
        [Required]
        public byte Type { get; set; }
        public virtual ChecklistMain CheckListMain { get; set; }
    }
}