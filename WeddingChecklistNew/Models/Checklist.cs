using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingChecklistNew.Models
{
    public class Checklist
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Product Url")]
        public string Url { get; set; }
        [Required]
        [DisplayName("Product Price")]
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Product Priority")]
        public Int16 Priority { get; set; }
        public virtual ChecklistMain CheckListMain { get; set; }
        [Required]
        [DisplayName("CheckList Name")]
        public int ChecklistMainId { get; set; }
        public IEnumerable<ChecklistImage> CheckListImage { get; set; }
    }
}