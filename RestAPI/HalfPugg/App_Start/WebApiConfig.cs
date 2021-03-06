﻿using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HalfPugg
{
    public static class WebApiConfig
    {
        public static string tokenHeader = "token-jwt";

        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
          
        
            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            GlobalConfiguration.Configuration.Formatters
                   .JsonFormatter.SerializerSettings.Re‌​ferenceLoopHandling
                   = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }
    }
}
