using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingChecklistNew.Controllers
{
    public class clsBaseController : Controller
    {

        protected JsonResult ThrowJsonError(Exception e)
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            Response.StatusDescription = e.Message;
            return Json(new { Message = e.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}