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
        private AccountController mAccountController = new AccountController();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            if (Request.Cookies["token"]!= null)
            {
                mAccountController.InitializeController(this.Request.RequestContext);
                TempData["username"] = mAccountController.GetLoginUserName();
            }
            return View();
        }
        public PartialViewResult GetSlider()
        {
            return PartialView("_GetSlider");
        }
    }
}
