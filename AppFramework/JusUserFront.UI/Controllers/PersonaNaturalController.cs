using System;
using System.Web.Mvc;
using JusNucleo.Bl.Personas;
using JusUserFront.UI.BaseUI;

namespace JusUserFront.UI.Controllers
{
    public class PersonaNaturalController : JusControllerBase
    {
        //
        // GET: /PersonaNatural/
        public ActionResult Index()
        {
            return View(PersonaNaturalCriteria.New());
        }

        [HttpPost]
        public ActionResult Index(PersonaNaturalCriteria criteria)
        {
            if (ModelState.IsValid)
            {
                criteria.Buscar();
            }
            return View(criteria);
        }

        [HttpPost]
        [Csla.Web.Mvc.HasPermission(Csla.Rules.AuthorizationActions.CreateObject, typeof(PersonaNatural))]
        public ActionResult Create(PersonaNatural persona,FormCollection collection)
        {
            var correosList = collection["correo"];
            if (correosList == null)
            {
                return View(persona);
            }
            var correos = correosList.Split(',');
            var isErasers = collection["isEraser"].Split(',');
            for (int i=0; i< correos.Length;i++)
            {
                if (!Boolean.Parse(isErasers[i]) && correos[i]!=string.Empty)
                {
                    var mail = PersonaCorreo.New();
                    mail.Correo = correos[i];
                    persona.Correos.Add(mail);
                }
            }

            if (persona.IsValid)
            {
                if (SaveObject(persona, false))
                {
                    return View();
                }
                
            }
            return View(persona);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int personaId)
        {
            ViewData.Model=PersonaNatural.Get(personaId);
            return View();
        }

        public ActionResult CrearCorreo()
        {
            var correo = PersonaCorreo.New();
            return View("PersonaCorreo",correo);
        }

        [HttpPost]
        public ActionResult CrearCorreo(PersonaCorreo correo)
        {
            return PartialView("PersonaCorreo", correo);
        }

	}
}