using System.Web.Mvc;
using System.Web.Routing;

namespace Calculator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Calculator",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Calculator", action = "Calculator", id = UrlParameter.Optional });
        }
    }
}