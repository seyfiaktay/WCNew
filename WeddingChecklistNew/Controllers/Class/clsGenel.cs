using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingChecklistNew.Controllers.Class
{
    public static class clsGenel
    {
        public static int commentsCount = 0;
        public static int commentsPageCount = 10;
        public enum ProductPriority
        {
            Lowest = 1,
            Low = 2,
            Normal = 3,
            High = 4,
            Highest = 5,
        }
        public const string cnstWebsiteURL = "http://createweddinglist.com";
        //public const string cnstWebsiteURL = "http://localhost:55465";
    }
}