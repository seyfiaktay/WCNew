using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeddingChecklistNew.Models.Data_Attribute;
using static WeddingChecklistNew.Models.Enum.clsEnum;

namespace WeddingChecklistNew.Models
{
    public class Checklistdo : clsBase
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Product Url")]
        public string Url { get; set; }
        [DisplayName("Product Price")]
        public decimal? Price { get; set; }
        public virtual Currency Currency { get; set; }
        [DisplayName("Product Currency")]
        public int? CurrencyId { get; set; }
        [Required]
        [DisplayName("Product Priority")]
        [Range(1, int.MaxValue, ErrorMessage = "The Priority field is required.")]
        public ProductPriority Priority { get; set; }
        public virtual ChecklistMain CheckListMain { get; set; }
        [Required]
        [DisplayName("CheckList Name")]
        public int ChecklistMainId { get; set; }
        public IEnumerable<ChecklistImage> CheckListImage { get; set; }

        [DisplayName("Image Url")]
        [ImageValidationAttribute(ErrorMessage = "Image must be .jpg")]
        public string ImageUrl { get; set; }
    }
}