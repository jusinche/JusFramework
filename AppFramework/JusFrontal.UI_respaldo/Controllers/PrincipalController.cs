using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JusFrontal.UI.BaseUI;

namespace JusFrontal.UI.Controllers
{
    public class PrincipalController : JusControllerBase
    {
        //
        // GET: /Principal/
        public ActionResult Index()
        {
            var user = UsuarioActual;
            return PartialView();
        }
	}
}