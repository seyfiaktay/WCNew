using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeddingChecklistNew.Models.Data_Attribute
{
    public class ImageValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string strValue = value as string;
            if (!string.IsNullOrEmpty(strValue))
            {
                if (!strValue.Contains(".jpg"))
                {
                    return false;
                }
            }
            return true;
        }
    }
}