using System;
using System.Collections.Generic;
using System.Data;

namespace Jus.Plataforma.Dal
{
    ///<summary>
    /// Define una clase abstracta que representa una base de datos
    ///</summary>
    public abstract class Database : IDisposable, IDatabase
    {
        protected IDbConnection _activeConnection;
        protected List<IDbCommand> _createdCmds;
        protected bool _disposed;
        ITraslateDataBaseException _traslateException;

        /////<summary>
        ///// Constructor 
        /////</summary>
        /////<param name="traslateDataBaseException"></param>
        //protected Database(ITraslateDataBaseException traslateDataBaseException)
        //{
        //    TraslateException = traslateDataBaseException;
        //}


        protected string connectionString;

        #region Abstract Functions

        public abstract IDbConnection CreateConnection();

        public abstract IDbCommand CreateSPCommand(string cmdName);

        public virtual IDbCommand CreateSPCommand(string cmdName, IDbConnection cn)
        {
            var cm = cn.CreateCommand();
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandText = cmdName;
            _createdCmds.Add(cm);
            return cm;
        }

        public abstract IDataReader ExecuteDataReader(IDbCommand command);
        public abstract DataSet ExecuteDataSet(IDbCommand command);
        public abstract void AddParameterWithValue(IDbCommand command, string paramName, object value);
        public abstract void AddParameterWithValue(IDbCommand command, string paramName, DbType type, object value);

        public abstract void AddParameterWithValue(IDbCommand command, string paramName, DbType type,
                                                   ParameterDirection direction, object value);

        public abstract void AddParameter(IDbCommand command, string paramName, DbType type);
        public abstract void AddParameter(IDbCommand command, string paramName, DbType type, int size);

        public abstract void AddParameter(IDbCommand command, string paramName, DbType type,
                                          ParameterDirection direction);

        public abstract void AddParameter(IDbCommand command, string paramName, DbType type,
                                          ParameterDirection direction, int size);

        public abstract void AddParameterArray(IDbCommand command, ParameterDirection direction, string paramName,
                                               object parameters, DbType type);

        public abstract void AddParameterArray(IDbCommand command, ParameterDirection direction, string paramName,
                                               object parameters, DbType type, int size);

        public abstract void AddParameterArray(IDbCommand command, ParameterDirection direction, string paramName,
                                               DbType type, int size);

        public abstract object GetOutputParameterValue(IDbCommand command, string paramName);
        public abstract int ExecuteNonQuery(IDbCommand command);
        public abstract object ExecuteScalar(IDbCommand command);

        #endregion

        /// <summary>
        /// Para realizar traducciones de excepciones especificas del proveedor de base de datos a clases genericas de excepciones de base de datos
        /// </summary>
        protected TraslateDataBaseOracleException TraslateException
        {
            get { return _traslateException; }
            set { _traslateException = value; }
        }

        #region IDatabase Members

        public virtual string ConnectionString
        {
            get { return connectionString; }
            protected set { connectionString = value; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected void OpenConnection()
        {
            if (_activeConnection.State != ConnectionState.Open)
                _activeConnection.Open();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    foreach (var cmd in _createdCmds)
                        cmd.Dispose();
                    _createdCmds.Clear();
                    if (_activeConnection != null && _activeConnection.State != ConnectionState.Closed)
                        _activeConnection.Close();
                }
            }
            _disposed = true;
        }

        ~Database()
        {
            Dispose(false);
        }
    }
}