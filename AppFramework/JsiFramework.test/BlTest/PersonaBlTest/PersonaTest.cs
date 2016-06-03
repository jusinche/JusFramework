using System;
using System.Linq;
using JusFramework.InyeccionDependencia;
using JusNucleo.Bl.Comun;
using JusNucleo.Bl.Personas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JusFramework.test.BlTest.PersonaBlTest
{
    [TestClass]
    public class PersonaTest
    {
        [TestMethod]
        public void PersonaCreate()
        {
            RegisterDependency.Init();

            var persona = PersonaNatural.New();
            persona.FechaNacimiento=DateTime.Now.AddYears(-5);
            persona.Identificacion = "1103776313";
            persona.PrimerApellido = "Sinche";
            persona.SegundoApellido = "Salinas";
            persona.PrimerNombre = "Junior";
            persona.SegundoNombre = "Ulises";

            var t1 = DateTime.Now.Ticks;
            persona.TipoIdentificacion = CatalogoItemList.Get(CatalogoConstantes.CatIdentificacionTipo).First(x => x.Codigo == CatalogoConstantes.IdentificacionCedula).Id;

            var t2 = DateTime.Now.Ticks;
            persona.Genero =CatalogoItemList.Get(CatalogoConstantes.CatGenero).First(x => x.Codigo == CatalogoConstantes.GeneroMasculino).Id;
            var t3 = DateTime.Now.Ticks;
            persona.EstadoCivil = CatalogoItemList.Get(CatalogoConstantes.CatEstadoCivil).First(x => x.Codigo == CatalogoConstantes.EstadoCivilUnionlibre).Id;

            var t4 = DateTime.Now.Ticks;

            System.Diagnostics.Trace.WriteLine(string.Format("Tiempos:{0}, {1}, {2}, {3}", t1, t2, t3, t4));

            System.Diagnostics.Trace.WriteLine("T1:" + ((t2 - t1) / 1000));
            System.Diagnostics.Trace.WriteLine("T2:" + ((t3 - t2) / 1000));
            System.Diagnostics.Trace.WriteLine("T3:" + ((t4 - t3) / 1000));
            System.Diagnostics.Trace.WriteLine("T3:" + ((t4 - t1) / 1000));

             persona.Save();

            var personaCriteria = PersonaNaturalCriteria.New();
            var personaList = PersonaNaturalList.Get(personaCriteria);

            var id=personaList.First(x => x.Identificacion == persona.Identificacion).Id;

            var persona1 = PersonaNatural.Get(id);

           

            Assert.AreEqual(persona1.Identificacion, persona.Identificacion);
            Assert.AreNotEqual(persona1.Id, 0);
            Assert.AreNotEqual(personaList.Count, 0);

            persona1.Delete();
            personaCriteria.Identificacion = persona1.Identificacion;

            var personaList1 = PersonaNaturalList.Get(personaCriteria);
            Assert.AreEqual(personaList1.Count, 0);
        }

        [TestMethod]
        public void PersonaGetList()
        {

            RegisterDependency.Init();
            var personaCriteria = PersonaNaturalCriteria.New();
            var personaList = PersonaNaturalList.Get(personaCriteria);

            var id = personaList.First().Id;

            //var persona1 = PersonaNatural.Get(id);
            Assert.AreNotEqual(personaList.Count, 0);
        }
    }
}
