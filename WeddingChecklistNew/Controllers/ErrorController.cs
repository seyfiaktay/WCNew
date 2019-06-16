using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingChecklistNew.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HttpError404(string message)
        {
            ViewBag.message = message;
            ViewBag.error_code = 404;
            return View("Error");
        }

        public ActionResult HttpError500(string message)
        {
            ViewBag.message = message;
            ViewBag.error_code = 500;
            return View("Error");
        }

        public ActionResult General(string message)
        {
            ViewBag.error_code = 404;
            ViewBag.message = message;
            return View("Error");
        }
    }
}