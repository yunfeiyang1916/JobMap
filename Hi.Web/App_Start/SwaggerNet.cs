using Niusys.WebAPI.App_Start;
using Swagger.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SwaggerNet), "PreStart")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(SwaggerNet), "PostStart")]
namespace Niusys.WebAPI.App_Start
{
    public static class SwaggerNet
    {
        public static void PreStart()
        {
            RouteTable.Routes.MapHttpRoute(
                name: "SwaggerApi",
                routeTemplate: "api/docs/{controller}/{action}",
                defaults: new { swagger = true }
            );
        }

        public static void PostStart()
        {
            var config = GlobalConfiguration.Configuration;
            config.Filters.Add(new SwaggerActionFilter());
        }
    }
}