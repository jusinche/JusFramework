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
        public ActionResult Create(PersonaNatural persona)
        {
            if (ModelState.IsValid)
            {
                persona.Save();
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

	}
}