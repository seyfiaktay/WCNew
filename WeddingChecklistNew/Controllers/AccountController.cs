using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WeddingChecklistNew.Models;

namespace WeddingChecklistNew.Controllers
{
    public class AccountController : Controller
    {
        private APIAccountController aPIAccountController = new APIAccountController();
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPasswordEmail(string userId,string code)
        {
            return View();
        }

        [HttpGet]
        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string email)
        {
            return View();
        }

    }
}