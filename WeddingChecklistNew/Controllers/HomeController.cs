using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingChecklistNew.Models;

namespace WeddingChecklistNew.Controllers
{
    public class HomeController : Controller
    {
        private APIAccountController mAPIAccountController = new APIAccountController();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
        public PartialViewResult GetSlider()
        {
            return PartialView("_GetSlider");
        }
    }
}
