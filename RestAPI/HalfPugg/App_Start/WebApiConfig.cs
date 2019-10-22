using HalfPugg.Models;
using System.IdentityModel.Tokens;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HalfPugg
{
    public static class WebApiConfig
    {
        public static SigningCredentials singingCredentials;

        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            var mySecret = "This is my shared, not so secret, secret!";
            var signKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(mySecret));
            singingCredentials = new SigningCredentials(signKey,
                SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }
    }
}
