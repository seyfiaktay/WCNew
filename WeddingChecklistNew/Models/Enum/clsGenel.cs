using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingChecklistNew.Models.Enum
{
    public static class clsGenel
    {
        public enum ProductPriority
        {
            Lowest = 1,
            Low = 2,
            Normal = 3,
            High = 4,
            Highest = 5,
        }
        public const string cnstWebsiteURL = "https://weddingchecklistnew.azurewebsites.net";
        //public const string cnstWebsiteURL = "http://localhost:55465";
    }
}