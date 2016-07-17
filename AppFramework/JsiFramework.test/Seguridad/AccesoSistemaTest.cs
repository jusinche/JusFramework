using System;
using JusFramework.InyeccionDependencia;
using JusNucleo.Bl.Sistema.Logeo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JusFramework.test.Seguridad
{
    [TestClass]
    public class AccesoSistemaTest
    {
        [TestMethod]
        public void Logear()
        {
            RegisterDependency.Init();
            Assert.AreEqual(true, LoginCmd.Execute("admin", "Adm1nAdm1n").Identity.IsAuthenticated);
        }

        [TestMethod]
        public void GetUsuario()
        {
            RegisterDependency.Init();
            var usuario = Cuenta.Get("admin");

            Assert.AreEqual(false, usuario==null);
        }

        [TestMethod]
        public void ActualizarUsuario()
        {
            RegisterDependency.Init();
            var usuario = Cuenta.Get("admin");

            usuario.EnLinea = false;
            usuario.Accesos = usuario.Accesos + 1;
            usuario.UltimoAcceso = DateTime.Now;

            var user= usuario.Save();
            Assert.AreEqual(false, user == null);
            Assert.AreEqual(true, user != null && usuario.Accesos == user.Accesos);
        }


         
    }
}
