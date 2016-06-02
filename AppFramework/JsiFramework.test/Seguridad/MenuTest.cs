using System;
using System.Linq;
using JusFramework.InyeccionDependencia;
using JusNucleo.Bl.Sistema.Menu;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JusFramework.test.Seguridad
{
    [TestClass]
    public class MenuTest
    {
        [TestMethod]
        public void ObtenerMenu()
        {
            RegisterDependency.Init();
            var criteria = ModuloFuncionalidadCriteria.New();
            criteria.Usuario = "admin";

            System.Diagnostics.Trace.WriteLine("Test Cache");

            var t1 = DateTime.Now.Ticks;

            var funcionalidades=ModuloFuncionalidadList.Get(criteria);

            var t2 = DateTime.Now.Ticks;

            var funcionalidades1 = ModuloFuncionalidadList.Get(criteria);

            var t3 = DateTime.Now.Ticks;

            var funcionalidades3 = ModuloFuncionalidadList.Get(criteria);

            var t4 = DateTime.Now.Ticks;

            System.Diagnostics.Trace.WriteLine(string.Format("Tiempos:{0}, {1}, {2}, {3}",t1, t2,t3,t4));

            System.Diagnostics.Trace.WriteLine("T1:"+((t2-t1)/1000));
            System.Diagnostics.Trace.WriteLine("T2:" + ((t3 - t2)/1000));
            System.Diagnostics.Trace.WriteLine("T3:" + ((t4 - t3)/1000));

            Assert.AreEqual(funcionalidades1.Count, funcionalidades.Count);
            Assert.AreEqual(funcionalidades3.Count, funcionalidades.Count);
            Assert.AreEqual(funcionalidades1.First().Id, funcionalidades.First().Id);
            Assert.AreEqual(funcionalidades3.First().Id, funcionalidades.First().Id);
        }
    }
}
