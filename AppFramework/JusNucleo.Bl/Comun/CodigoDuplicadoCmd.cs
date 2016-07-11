using System;
using System.Data;
using Csla;
using JusFramework.Bl;

namespace JusNucleo.Bl.Comun
{
    [Serializable]
    public class CodigoDuplicadoCmd : JusCommandBase<CodigoDuplicadoCmd> 
    {
        #region Client-side Code

        //para los parametros de entrada
        private int _id;
        private string _valor;
        private string _sp;


        public string Result { get; private set; }

        /// <summary>
        /// Parametro de salida
        /// </summary>
        /// <summary>
        /// Ejecutar el metodo para validar si un usuario es correcto
        /// </summary>
        /// <param name="id">identificador del valor a comparar</param>
        /// <param name="valor">valor a comparar</param>
        /// <param name="sp">Nombre del store procedure</param>
        /// <returns></returns>
        public static string Execute(int id, string valor, string sp)
        {
            var cmd = new CodigoDuplicadoCmd{_id = id,_valor = valor,_sp = sp};
            cmd = DataPortal.Execute(cmd);
            return cmd.Result;
        }


        private CodigoDuplicadoCmd()
        { /* require use of factory methods */ }

        #endregion

        

        protected override string NombreProcedimiento
        {
            get { return _sp; }
        }

        protected override void AddParameteres()
        {
            Db.AddParameterWithValue(Comando, "en_id", DbType.Int32, _id);
            Db.AddParameterWithValue(Comando, "ec_valor", DbType.String, _valor);
            Db.AddParameter(Comando, "sn_cant", DbType.Int32, ParameterDirection.Output);
            Db.AddParameter(Comando, "sc_mensaje", DbType.String, ParameterDirection.Output,500);
        }

        protected override void PostExecute()
        {
            int existe;
            Int32.TryParse(Db.GetOutputParameterValue(Comando, "sn_cant").ToString(), out existe);
            Result= Db.GetOutputParameterValue(Comando, "sc_mensaje").ToString();
        }
    }
}