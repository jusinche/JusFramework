using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using JusNucleo.Bl.Sistema.Logeo;
using JusUserFront.UI.BaseUI;
using JusUserFront.UI.Domain;
using JusUserFront.UI.Models;

namespace JusUserFront.UI.Controllers
{
    public class JusHomeController : JusControllerBase
    {
        //
        // GET: /JusHome/
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult JusNavbar(string user)
        {
            var menu = new MenuItems();
            return View("JusNavbar", menu.GetMenuItems(user).ToList());
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            //ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = LoginCmd.Execute(model.UserName, model.Password);
                    if (user != null)
                    {

                        FormsAuthentication.SetAuthCookie(user.Identity.Name, false);
                        if (ControllerContext.HttpContext.Session != null)
                            ControllerContext.HttpContext.Session["user"] = model.UserName;

                        //System.Web.HttpContext.Current.User =new System.Security.Principal.GenericPrincipal()
                        FormsAuthentication.RedirectFromLoginPage(model.UserName, false);
                        return RedirectToLocal("~/JusHome/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuario o clave Incorrecta");
                    }
                }

                // If we got this far, something failed, redisplay form
                return null; //System.Web.UI.WebControls.View(model);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ManejadorExcepciones.GetMensaje(ex));
                //Flash.Error();

            }
            return null;
            //return System.Web.UI.WebControls.View(model);
        }

        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "JusHome");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                //return RedirectToAction("Index", "Index");
            }
            return Redirect(returnUrl);
        }
	}
}