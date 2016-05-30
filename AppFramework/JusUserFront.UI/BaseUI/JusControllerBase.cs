using System;
using System.Web.Mvc;
using System.Web.Security;
using JusUserFront.UI.ManejadorExcepciones;
using JusNucleo.Bl.Sistema.Logeo;
using Microsoft.Practices.ServiceLocation;

namespace JusUserFront.UI.BaseUI
{
    [HandleError]
    public class JusControllerBase : Controller
    {

        /*[SetterProperty]
            public IPapyrusApp PapyrusApp { get; set; }

            protected User UsuarioActual
            {
                get { return PapyrusApp.Usuario(); }
            }

            protected Emisor EmisorActual
            {
                get { return PapyrusApp.Emisor(); }
            }*/

        


        protected Cuenta UsuarioActual
        {
            get { return CurrentUser(); }
        }

        private Cuenta _usuario;
        private Cuenta CurrentUser()
        {
            if (Request.IsAuthenticated)
            {
                if (_usuario==null)
                {
                    _usuario=Cuenta.Get(System.Web.HttpContext.Current.User.Identity.Name);
                }
                return _usuario;
            }
            return null;
        }


        private readonly IManejadorMensajes _manejadorExcepciones = ServiceLocator.Current.GetInstance<IManejadorMensajes>();
        protected IManejadorMensajes ManejadorExcepciones
            {
                get { return _manejadorExcepciones; }
            }

        /// <summary>
        /// This provides simple feedback to the modelstate in the case of errors.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is RedirectToRouteResult)
            {
                try
                {
                    // put the ModelState into TempData
                    TempData.Add("_MODELSTATE", ModelState);
                }
                catch (Exception)
                {
                    TempData.Clear();
                    // swallow exception
                }
            }
            else if (filterContext.Result is ViewResult && TempData.ContainsKey("_MODELSTATE"))
            {
                // merge modelstate from TempData
                var modelState = TempData["_MODELSTATE"] as ModelStateDictionary;
                if (modelState != null)
                {
                    foreach (var item in modelState)
                    {
                        if (!ModelState.ContainsKey(item.Key))
                            ModelState.Add(item);
                    }
                }
            }
            
        }
    }
}