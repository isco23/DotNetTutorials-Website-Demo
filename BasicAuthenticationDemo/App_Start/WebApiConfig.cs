﻿using BasicAuthenticationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BasicAuthenticationDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            // Web API configuration and services
            //config.Filters.Add(new BasicAuthenticationAttribute());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
