using System;
using System.Data;
using Csla;
using JusFramework.Dal;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusCommandBase<T> : CommandBase<T> where T : JusCommandBase<T>
    {
        #region Server-side Code

        protected DatabaseConection Db;
        protected IDbCommand Comando;

        /// <summary>
        /// Nombre del procedimiento almacenado que se va a ejecutar desde la base de datos.
        /// </summary>
        protected abstract string NombreProcedimiento { get; }

        protected override void DataPortal_Execute()
        {
            Db = DatabaseFactory.CreateDatabase();
            Comando = Db.CreateSPCommand(NombreProcedimiento);
            AddParameteres();
            Db.ExecuteNonQuery(Comando);
            PostExecute();
        }

        /// <summary>
        /// Agregar los parametros para ejecutarlo en la base de datos
        /// </summary>
        protected abstract void AddParameteres();

        /// <summary>
        /// Recoge los datos posterior a la ejecucion de la base de datos.
        /// </summary>
        protected abstract void PostExecute();

        #endregion
    }
}
