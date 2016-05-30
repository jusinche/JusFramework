using System.Web.Mvc;
using System.Web.Routing;

namespace JusUserFront.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah.axd");
            routes.MapRoute(
                 "Default", // Route name
                 "{controller}/{action}/{id}", // URL with parameters
                 new { controller = "JusHome", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );

          //  new { controller = "Account", action = "Index" , id = UrlParameter.Optional} // Parameter defaults

            routes.IgnoreRoute("elmah.axd");
        }



    }
}
