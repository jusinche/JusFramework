using System;
using System.Data;
using System.Security.Principal;
using Csla;
using JusFramework.Bl;
using JusFramework.Encriptacion;
using JusNucleo.Bl.Comun;
using JusNucleo.Bl.Sistema.Aplicacion;
using Microsoft.Practices.ServiceLocation;

namespace JusNucleo.Bl.Sistema.Logeo
{
    [Serializable]
    public class LoginCmd : JusCommandBase<LoginCmd> 
    {
        #region Client-side Code

        //para los parametros de entrada
        private string _usuario;
        private string _clave;


        public IPrincipal Result { get; private set; }

        /// <summary>
        /// Parametro de salida
        /// </summary>
        



        #endregion

        #region Factory Methods

        /// <summary>
        /// Ejecutar el metodo para validar si un usuario es correcto
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public static IPrincipal Execute(string usuario, string clave)
        {
            LoginCmd cmd = new LoginCmd();
            cmd._usuario = usuario;

            cmd._clave = ServiceLocator.Current.GetInstance<IEncripta>().HashKey(clave);
            cmd = DataPortal.Execute(cmd);

            return cmd.Result;
        }

       
        private LoginCmd()
        { /* require use of factory methods */ }

        #endregion

        

        protected override string NombreProcedimiento
        {
            get { return ProcedimientosConstantes.PrcCuentaObtCant; }
        }

        protected override void AddParameteres()
        {
            Db.AddParameterWithValue(Comando, "ec_usuario", DbType.String, _usuario);
            Db.AddParameterWithValue(Comando, "ec_clave", DbType.String, _clave);
            Db.AddParameter(Comando, "sn_cantidad", DbType.Int32, ParameterDirection.Output);
        }

        protected override void PostExecute()
        {
            int existe;
            Int32.TryParse(Db.GetOutputParameterValue(Comando, "sn_cantidad").ToString(), out existe);
            if (existe != 0)
            {
                var app = new JusApplication();
                ((JusIdentity)app.Identity).SetCuenta(Cuenta.Get(_usuario));
                Result = app;
            }
        }
    }
}