using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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
                if (strValue.Contains(".jpg") || strValue.Contains(".png"))
                {
                    try
                    {
                        //valid url check
                        WebClient client = new WebClient();
                        client.DownloadString(strValue);
                        client.Dispose();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }    
            }
            else
            {
                return true;
            }
            return false;
        }
    }
}