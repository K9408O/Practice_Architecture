using System.Web.Http;
using System.Web.Http.Cors;

namespace Architecture.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {   // 🔥 允許所有來源存取 API
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
