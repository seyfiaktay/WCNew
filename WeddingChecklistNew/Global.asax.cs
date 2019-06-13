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
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
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
