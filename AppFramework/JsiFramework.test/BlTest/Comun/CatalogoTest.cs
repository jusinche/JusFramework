using System;
using JusFramework.InyeccionDependencia;
using JusNucleo.Bl.Comun;
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

            var t1 = DateTime.Now.Ticks;
            CatalogoItemList.Get(CatalogoConstantes.CatIdentificacionTipo);
            var t2 = DateTime.Now.Ticks;
            CatalogoItemList.Get(CatalogoConstantes.CatGenero);
            var t3 = DateTime.Now.Ticks;
            CatalogoItemList.Get(CatalogoConstantes.CatEstadoCivil);
            var t4 = DateTime.Now.Ticks;

            System.Diagnostics.Trace.WriteLine(string.Format("Tiempos:{0}, {1}, {2}, {3}", t1, t2, t3, t4));
            System.Diagnostics.Trace.WriteLine("T1:" + ((t2 - t1) / 1000));
            System.Diagnostics.Trace.WriteLine("T2:" + ((t3 - t2) / 1000));
            System.Diagnostics.Trace.WriteLine("T3:" + ((t4 - t3) / 1000));
            System.Diagnostics.Trace.WriteLine("T3:" + ((t4 - t1) / 1000));

            t1 = DateTime.Now.Ticks;
            CatalogoItemList.Get(CatalogoConstantes.CatIdentificacionTipo);
             t2 = DateTime.Now.Ticks;
            CatalogoItemList.Get(CatalogoConstantes.CatGenero);
             t3 = DateTime.Now.Ticks;
            CatalogoItemList.Get(CatalogoConstantes.CatEstadoCivil);
             t4 = DateTime.Now.Ticks;

            System.Diagnostics.Trace.WriteLine(string.Format("Tiempos:{0}, {1}, {2}, {3}", t1, t2, t3, t4));
            System.Diagnostics.Trace.WriteLine("T1:" + ((t2 - t1) / 1000));
            System.Diagnostics.Trace.WriteLine("T2:" + ((t3 - t2) / 1000));
            System.Diagnostics.Trace.WriteLine("T3:" + ((t4 - t3) / 1000));
            System.Diagnostics.Trace.WriteLine("T3:" + ((t4 - t1) / 1000));

            Assert.AreNotEqual(t4, 0);
            Assert.AreNotEqual(t4, 0);
        }
    }
}
