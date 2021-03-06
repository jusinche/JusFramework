using System;
using System.Configuration;
using Csla;
using JusFramework;
using JusFramework.Bl;
using JusFramework.Cache;

namespace JusNucleo.Bl.Sistema.Menu
{
    [Serializable]
    public class ModuloFuncionalidadCriteria : JusCriteriaBase<ModuloFuncionalidadCriteria, ModuloFuncionalidadList, ModuloFuncionalidadInfo>, ICacheable
    {
        #region Business Methods

        
        public string SistemaCodigo
        {
            get { return ConfigurationManager.AppSettings[ConstantesFramework.AplicacionCodigo]; }
        }

        public static readonly PropertyInfo<string> UsuarioProperty = RegisterProperty<string>(p => p.Usuario);
        public string Usuario
        {
            get { return GetProperty(UsuarioProperty); }
            set { SetProperty(UsuarioProperty, value); }
        }

        #endregion
        private ModuloFuncionalidadCriteria()
        { /* Require use of factory methods */ }


        public override string GetKey()
        {
            return string.Format("{0}_{1}_{2}", GetType(), SistemaCodigo, Usuario);
        }


       
    }
}
