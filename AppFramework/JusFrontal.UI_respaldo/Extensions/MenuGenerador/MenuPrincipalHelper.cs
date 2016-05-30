using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JusFrontal.UI.Extensions.MenuGenerador
{
    public  class MenuPrincipalHelper
    {
        public static MvcHtmlString GetMenuPrincipal()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                
                return new MvcHtmlString("jusinche");

            }
            return new MvcHtmlString(string.Empty);
        }

        
    }
}