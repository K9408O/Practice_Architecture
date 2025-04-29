using System.Web.Http;
using System.Web.Http.Cors;

namespace Architecture.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {   // 🔥 允許所有來源存取 API
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);
            var cors = new EnableCorsAttribute(
                origins: "https://localhost:44348",   // 允許的前端網址
                headers: "*",                        // 允許所有標頭
                methods: "*");                       // 允許所有方法 (GET/POST…)
            cors.SupportsCredentials = false;         // 如需帶 Cookie/JWT 再改 true
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
