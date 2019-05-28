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
        private APIAccountController mAPIAccountController = new APIAccountController();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}