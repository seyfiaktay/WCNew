using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WeddingChecklistNew.Models;

namespace WeddingChecklistNew.Controllers
{
    public class AccountController : Controller
    {
        private APIAccountController aPIAccountController = new APIAccountController();

        public void InitializeController(RequestContext context)
        {
            base.Initialize(context);
        }

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
            Request.Cookies["token"].Expires = DateTime.Now.AddDays(-1);
            var c = new HttpCookie("token");
            c.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(c);
            return RedirectToAction("Index", "Home");
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


        [HttpPost]
        public ActionResult Login(LoginBindingModel model)
        {
            try
            {
                string baseAddress = "http://localhost:55465";
                using (var client = new HttpClient())
                {
                    var form = new Dictionary<string, string>
                    {
                       {"grant_type", "password"},
                       {"username", model.Email},
                       {"password", model.Password},
                    };
                    var tokenResponse = client.PostAsync(baseAddress + "/token", new FormUrlEncodedContent(form)).Result;
                    if (tokenResponse.StatusCode != HttpStatusCode.OK)
                    {
                        return new HttpStatusCodeResult(tokenResponse.StatusCode,"please check your email and password");
                    }
                    var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                    HttpCookie httpCookie = new HttpCookie("token", token.AccessToken);
                    httpCookie.Expires = DateTime.Now.AddDays(14);
                    Response.Cookies.Add(httpCookie);
                    return View();
                }
            }
            catch
            {
                return View("Error");
            }
        }

        public string GetLoginUserName()
        {
            string baseAddress = "http://localhost:55465";
            using (var client = new HttpClient())
            {
                string token = Request.Cookies["token"].Value.ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = client.GetAsync(baseAddress + "/api/Account/UserInfo").Result;
                var userinfo = responseMessage.Content.ReadAsAsync<UserInfoViewModel>(new[] { new JsonMediaTypeFormatter() }).Result;
                return userinfo.Email;
            }
        }
    }
}