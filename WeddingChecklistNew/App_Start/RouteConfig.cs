using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WeddingChecklistNew
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Checklists/Index",                                           // Route name
               "Checklists/Index/{checklistmainid}",                            // URL with parameters
               new { controller = "Checklists", action = "Index", checklistmainid = UrlParameter.Optional}  // Parameter defaults
           );

            routes.MapRoute(
               "Checklists/Create",                                           // Route name
               "Checklists/Create/{checklistmainid}",                            // URL with parameters
               new { controller = "Checklists", action = "Create", checklistmainid = UrlParameter.Optional }  // Parameter defaults
           );

            routes.MapRoute(
               "Checklists/Edit",                                           // Route name
               "Checklists/Edit/{checklistmainid}/{Id}",                            // URL with parameters
               new { controller = "Checklists", action = "Edit", checklistmainid = UrlParameter.Optional,Id=UrlParameter.Optional }  // Parameter defaults
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index",id=UrlParameter.Optional}
            );

            
        }
    }
}
