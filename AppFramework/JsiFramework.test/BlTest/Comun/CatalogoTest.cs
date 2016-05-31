using System;
using System.Linq;
using JusFramework.InyeccionDependencia;
using JusNucleo.Bl.Comun;
using JusNucleo.Bl.Personas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JusFramework.test.BlTest.Comun
{
    [TestClass]
    public class CatalogoTest
    {
        [TestMethod]
        public void GetCatalogo()
        {
            RegisterDependency.Init();

            var catalogo = CatalogoItemList.Get(CatalogoConstantes.CatIdentificacionTipo);

            
            Assert.AreNotEqual(catalogo.ToList().Count, 0);
            Assert.AreNotEqual(catalogo.First().Id, 0);
        }
    }
}
