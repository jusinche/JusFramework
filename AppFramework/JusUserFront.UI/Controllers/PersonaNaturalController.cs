using System;
using System.Collections.Generic;
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
        public ActionResult Create(PersonaNatural persona,FormCollection collection, List<PersonaCorreo> correos1 )
        {
            var correos = (collection["correo"]).Split(',');
            var isErasers = collection["isEraser"].Split(',');
            for (int i=0; i< correos.Length;i++)
            {
                if (!Boolean.Parse(isErasers[i]))
                {
                    var mail = PersonaCorreo.New();
                    mail.Correo = correos[i];
                    persona.Correos.Add(mail);
                }
            }

            

            if (ModelState.IsValid)
            {
                persona.Save();
                return View(PersonaNatural.New());
            }
            return View(persona);
        }

        public ActionResult Create()
        {
            return View(PersonaNatural.New());
        }

        public ActionResult Edit(int personaId)
        {
            PersonaNatural.Get(personaId);
            return View(PersonaNatural.Get(personaId));
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