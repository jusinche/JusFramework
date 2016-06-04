using System;
using System.Data;
using JusFramework.Bl;
using DataPortal = Csla.DataPortal;

namespace JusNucleo.Bl.Sistema.Menu
{
    [Serializable]
    public class ModuloFuncionalidadList :
      JusReadOnlyListBase<ModuloFuncionalidadList, ModuloFuncionalidadInfo>
    {

        #region Factory Methods

        public static ModuloFuncionalidadList Get(ModuloFuncionalidadCriteria criteria)
        {
            return DataPortal.Fetch<ModuloFuncionalidadList>(criteria);

            
        }

        private ModuloFuncionalidadList()
        { /* require use of factory methods */ }

        #endregion

        protected override string NombreProcedimiento
        {
            get { return "PKG_SEG_SEGURIDAD.PRC_OBT_FUNCIONALIDAD_MENU"; }
        }

        protected void AddParameterCriteria(ModuloFuncionalidadCriteria criteria)
        {
            Db.AddParameterWithValue(Comando, "ec_usuario", DbType.String, criteria.Usuario);
            Db.AddParameterWithValue(Comando, "ec_sistema", DbType.String, criteria.SistemaCodigo);
        }

    }

    [Serializable]
    public class ModuloFuncionalidadInfo : JusReadOnlyBase<ModuloFuncionalidadInfo>
    {
        #region Business Methods

        public int ModuloId { get; private set; }
        public string ModuloNombre { get; private set; }
        public string ModuloMenu { get; private set; }
        public string FuncionalidadNombre { get; private set; }

        public string FuncionalidadCodigo { get; private set; }
        public string FuncionalidadMenu { get; private set; }

        public string Controlador { get; private set; }
        public string Accion { get; private set; }
        public string Parametros { get; private set; }

        #endregion

        
        #region Factory Methods

       

        private ModuloFuncionalidadInfo()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        protected void Child_Fetch(IDataReader data)
        {
            Id = Convert.ToInt32(data["fun_id"]);
            ModuloId = Convert.ToInt32(data["mod_id"]);
            ModuloNombre = data["mod_nombre"].ToString();
            ModuloMenu = data["mod_nombre_menu"].ToString();
            FuncionalidadNombre = data["fun_nombre"].ToString();
            FuncionalidadCodigo = data["fun_codigo"].ToString();
            FuncionalidadMenu = data["fun_nombre_menu"].ToString();
            Controlador = data["fun_controlador"].ToString();
            Accion = data["fun_accion"].ToString();
            Parametros = data["fun_parametros"].ToString();
        }

        #endregion
    }
}
