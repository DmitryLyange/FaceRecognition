using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace FaceRecognition
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "Api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //var routes = config.Routes;

            //routes.MapHttpRoute("DefaultApiGet", "Api/{controller}/{id}", new { action = "Get", id = RouteParameter.Optional }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            //routes.MapHttpRoute("DefaultApiPost", "Api/{controller}/{id}", new { action = "Post", id = RouteParameter.Optional }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            //routes.MapHttpRoute("DefaultApiPut", "Api/{controller}/{id}", new { action = "Put", id = RouteParameter.Optional }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });
            //routes.MapHttpRoute("DefaultApiDelete", "Api/{controller}/{id}", new { action = "Delete", id = RouteParameter.Optional }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) });

        }
    }
}
