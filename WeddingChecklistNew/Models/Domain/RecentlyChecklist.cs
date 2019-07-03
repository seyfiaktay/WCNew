using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static WeddingChecklistNew.Controllers.Class.clsGenel;

namespace WeddingChecklistNew.Models.Domain
{
    public class RecentlyChecklist
    {
        [DisplayName("Main Name")]
        public string MainName { get; set; }
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [DisplayName("Product Url")]
        public string Url { get; set; }
        [DisplayName("Product Price")]
        public decimal? Price { get; set; }
        [DisplayName("Product Currency")]
        public string CurrencyName{ get; set; }
        [DisplayName("Product Priority")]
        public ProductPriority Priority { get; set; }
        public string ImagePath { get; set; }
        public string Link { get; set; }
        public string UserId { get; set; }
    }
}