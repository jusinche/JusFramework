using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Oracle.DataAccess.Client;

namespace Jus.Plataforma.Dal
{
    ///<summary>
    /// Implementación de acceso a base de datos para Oracle
    ///</summary>
    public class OracleDatabase : Database
    {
        public OracleDatabase(string conxString)
        {
            if (string.IsNullOrEmpty(conxString))
                throw new ArgumentException();


            TraslateException = new TraslateDataBaseOracleException();

            _activeConnection = new OracleConnection(conxString);
            _createdCmds = new List<IDbCommand>();
            ConnectionString = conxString;
        }

        public override IDbConnection CreateConnection()
        {
            IDbConnection newConnection = new OracleConnection(ConnectionString);

            return newConnection;
        }

        public override IDbCommand CreateSPCommand(string cmdName)
        {
            OracleCommand cm = (OracleCommand) _activeConnection.CreateCommand();
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandText = cmdName;
            cm.BindByName = true;
            _createdCmds.Add(cm);
            return cm;
        }

        public override IDbCommand CreateSPCommand(string cmdName, IDbConnection cn)
        {
            //Todo: Revisar codigo
            OracleCommand cm = (OracleCommand) cn.CreateCommand();
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandText = cmdName;
            cm.BindByName = true;
            return cm;
        }

        public override IDataReader ExecuteDataReader(IDbCommand command)
        {
            try
            {
                OpenConnection();
                IDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (OracleException oracleException)
            {
                //throw TraslateException.Handler(oracleException, command);
                throw;
            }
        }

        public override DataSet ExecuteDataSet(IDbCommand command)
        {
            try
            {
                OpenConnection();
                var adapter = new OracleDataAdapter(command as OracleCommand);
                var ds = new DataSet();
                adapter.SelectCommand.ExecuteNonQuery();
                adapter.Fill(ds);

                return ds;
            }
            catch (OracleException oracleException)
            {
                //throw TraslateException.Handler(oracleException, command);
                throw;
            }
        }

        public override void AddParameterWithValue(IDbCommand command, string paramName, DbType type,
                                                   ParameterDirection direction, object value)
        {
            var parameter = new OracleParameter();
            parameter.ParameterName = paramName;
            parameter.DbType = type;
            parameter.Direction = direction;

            if (value == null)
                parameter.Value = DBNull.Value;
            else
            {
                Type valueType = value.GetType();
                string smartDateValue = "";

                if (valueType.FullName == "Csla.SmartDate")
                    smartDateValue = (string)
                                     valueType.InvokeMember("Text",
                                                            BindingFlags.GetProperty | BindingFlags.Public |
                                                            BindingFlags.Instance, null, value, null);
                if (type == DbType.Date
                    && valueType.FullName == "Csla.SmartDate"
                    && string.IsNullOrEmpty(smartDateValue))
                    parameter.Value = DBNull.Value;
                else
                    parameter.Value = value;
            }

            ((OracleCommand) command).Parameters.Add(parameter);
        }

        public override void AddParameterWithValue(IDbCommand command, string paramName, object value)
        {
            ((OracleCommand) command).Parameters.Add(paramName, value);
        }

        public override void AddParameterWithValue(IDbCommand command, string paramName, DbType type, object value)
        {
            AddParameterWithValue(command, paramName, type, ParameterDirection.Input, value);
        }

        public override void AddParameter(IDbCommand command, string paramName, DbType type)
        {
            var parameter = new OracleParameter();
            parameter.ParameterName = paramName;
            parameter.DbType = type;

            ((OracleCommand) command).Parameters.Add(parameter);
        }

        public override void AddParameter(IDbCommand command, string paramName, DbType type, int size)
        {
            var parameter = new OracleParameter();
            parameter.ParameterName = paramName;
            parameter.DbType = type;
            parameter.Size = size;

            ((OracleCommand) command).Parameters.Add(parameter);
        }

        public override void AddParameter(IDbCommand command, string paramName, DbType type,
                                          ParameterDirection direction)
        {
            var parameter = new OracleParameter();
            parameter.ParameterName = paramName;
            parameter.Direction = direction;

            if (direction == ParameterDirection.Output && type == DbType.Object)
                parameter.OracleDbType = OracleDbType.RefCursor;
            else
                parameter.DbType = type;

            command.Parameters.Add(parameter);
        }

        public override void AddParameter(IDbCommand command, string paramName, DbType type,
                                          ParameterDirection direction, int size)
        {
            var parameter = new OracleParameter();
            parameter.ParameterName = paramName;
            parameter.DbType = type;
            parameter.Direction = direction;
            parameter.Size = size;

            command.Parameters.Add(parameter);
        }

        public override void AddParameterArray(IDbCommand command, ParameterDirection direction, string paramName,
                                               object parameters, DbType type)
        {
            var parameter = new OracleParameter();
            parameter.DbType = type;
            parameter.Direction = direction;
            parameter.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            parameter.ParameterName = paramName;
            parameter.Value = parameters;
            command.Parameters.Add(parameter);
        }

        public override void AddParameterArray(IDbCommand command, ParameterDirection direction, string paramName,
                                               DbType type, int size)
        {
            var parameter = new OracleParameter();
            parameter.DbType = type;
            parameter.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            parameter.ParameterName = paramName;
            parameter.Direction = direction;
            parameter.Size = size;
            command.Parameters.Add(parameter);
        }

        public override void AddParameterArray(IDbCommand command, ParameterDirection direction, string paramName,
                                               object parameters, DbType type, int size)
        {
            var parameter = new OracleParameter();
            parameter.DbType = type;
            parameter.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            parameter.ParameterName = paramName;
            parameter.Direction = direction;
            parameter.Value = parameters;
            parameter.Size = size;
            command.Parameters.Add(parameter);
        }


        public override object GetOutputParameterValue(IDbCommand command, string paramName)
        {
            return ((OracleCommand) command).Parameters[paramName].Value;
        }

        public override int ExecuteNonQuery(IDbCommand command)
        {
            try
            {
                OpenConnection();
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows;
            }
            catch (OracleException oracleException)
            {
                //throw TraslateException.Handler(oracleException, command);
                throw;
            }
        }

        public override object ExecuteScalar(IDbCommand command)
        {
            try
            {
                OpenConnection();
                object scalar = command.ExecuteScalar();
                return scalar;
            }
            catch (OracleException oracleException)
            {
                //throw TraslateException.Handler(oracleException, command);
                throw;
            }
        }
    }
}