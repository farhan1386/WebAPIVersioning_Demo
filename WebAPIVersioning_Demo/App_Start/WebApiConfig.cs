using System.Web.Http;
using System.Web.Http.Dispatcher;
using WebAPIVersioning_Demo.Custom;

namespace WebAPIVersioning_Demo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "Version1",
            //    routeTemplate: "api/v1/customer/{id}",
            //    defaults: new { id = RouteParameter.Optional, controller = "CustomerV1" }
            //    );

            //config.Routes.MapHttpRoute(
            //    name: "Version2",
            //    routeTemplate: "api/v2/customer/{id}",
            //    defaults: new { id = RouteParameter.Optional, controller = "CustomerV2" }
            //    );

            config.Services.Replace(typeof(IHttpControllerSelector),
                              new CustomControllerSelector(config));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
