//using System;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using System.Web.Security;
//using JusUserFront.UI.BaseUI;
//using JusUserFront.UI.Models;
//using JusNucleo.Bl.Sistema.Logeo;

////using MvcFlash.Core;

//namespace JusUserFront.UI.Controllers
//{
//    //[Authorize]
//    [HandleError]
//    public class AccountController : JusControllerBase
//    {
//        public AccountController()
//        // : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationContext())))
//        {
//        }



//        //
//        // GET: /Account/Login
//        [AllowAnonymous]
//        public ActionResult Login()
//        {
//            //ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }

//        public ActionResult Index()
//        {
//            //ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }

//        //
//        // POST: /Account/Login
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    var user = LoginCmd.Execute(model.UserName, model.Password);
//                    if (user != null)
//                    {

//                        FormsAuthentication.SetAuthCookie(user.Identity.Name, false);
//                        if (ControllerContext.HttpContext.Session != null)
//                            ControllerContext.HttpContext.Session["user"] = model.UserName;

//                        //System.Web.HttpContext.Current.User =new System.Security.Principal.GenericPrincipal()
//                        FormsAuthentication.RedirectFromLoginPage(model.UserName, false);
//                        return  RedirectToLocal("~/Account/Index");
//                    }
//                    else
//                    {
//                        ModelState.AddModelError("", "Usuario o clave Incorrecta");
//                    }
//                }

//                // If we got this far, something failed, redisplay form
//                return null; //System.Web.UI.WebControls.View(model);
//            }
//            catch (Exception ex)
//            {

//                ModelState.AddModelError("", ManejadorExcepciones.GetMensaje(ex));
//                //Flash.Error();

//            }
//            return null;
//            //return System.Web.UI.WebControls.View(model);
//        }
        

      

//        //
//        // POST: /Account/ExternalLogin
//       /* [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public ActionResult ExternalLogin(string provider, string returnUrl)
//        {
//            // Request a redirect to the external login provider
//            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
//        }*/

        
        

//        //
//        // POST: /Account/LogOff
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        public ActionResult LogOff()
//        {
//            FormsAuthentication.SignOut();
//            return RedirectToAction("Login", "Account");
//        }

        

        


       
//        public enum ManageMessageId
//        {
//            ChangePasswordSuccess,
//            SetPasswordSuccess,
//            RemoveLoginSuccess,
//            Error
//        }

//        private ActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            else
//            {
//                //return RedirectToAction("Index", "Index");
//            }
//            return Redirect(returnUrl);
//        }

//        private class ChallengeResult : HttpUnauthorizedResult
//        {
//            public ChallengeResult(string provider, string redirectUri)
//                : this(provider, redirectUri, null)
//            {
//            }

//            public ChallengeResult(string provider, string redirectUri, string userId)
//            {
//                LoginProvider = provider;
//                RedirectUri = redirectUri;
//                UserId = userId;
//            }

//            public string LoginProvider { get; set; }
//            public string RedirectUri { get; set; }
//            public string UserId { get; set; }

          
//        }
//    }
//}