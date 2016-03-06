using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TC2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            {
                //config.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
                config.Routes.MapHttpRoute(
                    name: "AccountApi",
                    routeTemplate: "api/{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional });


            }

        }
    }
}
