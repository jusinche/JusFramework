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

            var correo = persona.Correos.AddNew();
             correo.Correo = "juniorsin@gmail.com";

            
              persona.Correos.Add  (correo);
            var t1 = DateTime.Now.Ticks;
            persona.TipoIdentificacion = CatalogoItemList.Get(CatalogoConstantes.CatIdentificacionTipo).GetItem(CatalogoConstantes.IdentificacionCedula).Id;

            var t2 = DateTime.Now.Ticks;
            persona.Genero = CatalogoItemList.Get(CatalogoConstantes.CatGenero).GetItem(CatalogoConstantes.GeneroMasculino).Id;
            var t3 = DateTime.Now.Ticks;
            persona.EstadoCivil = CatalogoItemList.Get(CatalogoConstantes.CatEstadoCivil).GetItem( CatalogoConstantes.EstadoCivilUnionlibre).Id;

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

            persona1.EstadoCivil = CatalogoItemList.Get(CatalogoConstantes.CatEstadoCivil).GetItem(CatalogoConstantes.EstadoCivilUnionlibre).Id;
            persona1.Save();
            
            
            var persona2 = PersonaNatural.Get(id);
            persona2.Delete();
            persona2.Save();
           

            Assert.AreNotEqual(personaList.Count, 0);
            
            var personaList1 = PersonaNaturalList.Get(personaCriteria);
            Assert.AreNotEqual(personaList1.Count, personaList.Count);
        }

        [TestMethod]
        public void GetPersonaList()
        {
            RegisterDependency.Init();

            var t1 = DateTime.Now.Ticks;
            var personaCriteria = PersonaNaturalCriteria.New();
            personaCriteria.Identificacion = "001";
            var personaList = PersonaNaturalList.Get(personaCriteria);
            //var persona = PersonaNatural.Get(personaList.First().Id);
            var t2 = DateTime.Now.Ticks;
            System.Diagnostics.Trace.WriteLine("T2:" + ((t2 - t1) / 1000));
            Assert.AreNotEqual(personaList.Count, 0);
        }


        //[TestMethod]
        //public void GetPersonaCorreo()
        //{
        //    RegisterDependency.Init();

        //    var t1 = DateTime.Now.Ticks;
        //    var personaCriteria = PersonaNaturalCriteria.New();
        //    personaCriteria.Identificacion = "1103776313";
        //    var personaList = PersonaNaturalList.Get(personaCriteria);
        //    var persona = PersonaNatural.Get(personaList.First().Id);
        //    var leng = persona.Correos.Count;
        //    var t2 = DateTime.Now.Ticks;
        //    System.Diagnostics.Trace.WriteLine("T1:" + ((t2 - t1)/1000));
        //    Assert.AreNotEqual(persona.Correos.Count, 0);
        //    //var correo = PersonaCorreo.New();
        //    //correo.Correo = "jusinche@hotmail";
        //    //persona.Correos.Add(correo);
        //    persona.Correos.RemoveAt(1);
        //    var persona2=persona.Save();
        //    Assert.AreNotEqual(leng, 0);

        //    Assert.AreNotEqual(leng, persona2.Correos.Count);
        //}

        //[TestMethod]
        //public void DeletePersona()
        //{
        //    RegisterDependency.Init();

        //    var t1 = DateTime.Now.Ticks;
        //    var personaCriteria = PersonaNaturalCriteria.New();
        //    personaCriteria.Identificacion = "1103776313";
        //    var personaList = PersonaNaturalList.Get(personaCriteria);
        //    var persona = PersonaNatural.Get(personaList.First().Id);
            
        //    persona.Delete();
        //    persona.Save();

        //    Assert.AreNotEqual(personaList.Count, 0);
        //}

        /* [TestMethod]
        public void PersonaCreate1()
        {
            RegisterDependency.Init();

            var persona = PersonaNatural.New();
            persona.FechaNacimiento = DateTime.Now.AddYears(-5);
            persona.Identificacion = "1714326749";   
            persona.PrimerApellido = "SUARES";
            persona.SegundoApellido = "VALENCIA";
            persona.PrimerNombre = "ANGEL";
            persona.SegundoNombre = "PATRICIO";

            var t1 = DateTime.Now.Ticks;
            persona.TipoIdentificacion = CatalogoItemList.Get(CatalogoConstantes.CatIdentificacionTipo).GetItem(CatalogoConstantes.IdentificacionCedula).Id;

            var t2 = DateTime.Now.Ticks;
            persona.Genero = CatalogoItemList.Get(CatalogoConstantes.CatGenero).GetItem(CatalogoConstantes.GeneroMasculino).Id;
            var t3 = DateTime.Now.Ticks;
            persona.EstadoCivil = CatalogoItemList.Get(CatalogoConstantes.CatEstadoCivil).GetItem(CatalogoConstantes.EstadoCivilUnionlibre).Id;

            var t4 = DateTime.Now.Ticks;

            System.Diagnostics.Trace.WriteLine(string.Format("Tiempos:{0}, {1}, {2}, {3}", t1, t2, t3, t4));

            System.Diagnostics.Trace.WriteLine("T1:" + ((t2 - t1) / 1000));
            System.Diagnostics.Trace.WriteLine("T2:" + ((t3 - t2) / 1000));
            System.Diagnostics.Trace.WriteLine("T3:" + ((t4 - t3) / 1000));
            System.Diagnostics.Trace.WriteLine("T3:" + ((t4 - t1) / 1000));

            persona = persona.Save();

            Assert.AreNotEqual(persona.Id, 0);
        }*/
       
    }
}
