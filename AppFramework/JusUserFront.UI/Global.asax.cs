using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;
using JusFramework.InyeccionDependencia;
using JusUserFront.UI;
using Microsoft.Ajax.Utilities;

namespace JusUserFront.UI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterDependency.Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
           
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
           /* var user = string.Empty;
            if (this.Context.Session!=null)
            {
                user = this.Context.Session["user"].ToString();
            }
            
            if (user.ToString().IsEmpty())
            {
                if (!Request.Url.LocalPath.Contains("Account/"))
                {
                    Response.Redirect(string.Format("~/Account/Login"));
                }
            }*/
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}