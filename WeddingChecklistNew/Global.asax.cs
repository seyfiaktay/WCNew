using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WeddingChecklistNew.Controllers;

namespace WeddingChecklistNew
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "HttpError404";
                        break;
                    case 500:
                        // server error
                        action = "HttpError500";
                        break;
                    default:
                        action = "General";
                        break;
                }

                // clear error on server
                Server.ClearError();
                Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, "An error occured"));
            }
            else
            {
                // clear error on server
                Server.ClearError();
                Response.Redirect(String.Format("~/Error/{0}/?message={1}", "General", "An error occured"));
            }
            Logger.Error(User.Identity.Name + " kullanıcısında hata oluştu.",exception);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (Request.Cookies["email"] != null) {
                var authCookie = Request.Cookies["email"].Value;
                var bytes = Convert.FromBase64String(authCookie);
                var output = MachineKey.Unprotect(bytes, "ProtectCookie");
                string result = Encoding.UTF8.GetString(output);
                var identity = new GenericIdentity(result);
                var principal = new GenericPrincipal(identity, null);
                SetPrincipal(principal);
            }
        }

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }
}
