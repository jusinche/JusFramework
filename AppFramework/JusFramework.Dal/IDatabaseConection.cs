using System.Data;

namespace JusFramework.Dal
{
    /// <summary>
    /// Define los metodos para interactuar con una base de datos
    /// </summary>
    public interface IDatabaseConection
    {
        /// <summary>
        /// Cadena de conexion contra el proveedor de datos
        /// </summary>
        string ConnectionString { get; }


        IDbConnection CreateConnection();

        IDbCommand CreateSPCommand(string cmdName);
        IDbCommand CreateSPCommand(string cmdName, IDbConnection cn);
        IDataReader ExecuteDataReader(IDbCommand command);
        DataSet ExecuteDataSet(IDbCommand command);
        void AddParameterWithValue(IDbCommand command, string paramName, object value);
        void AddParameterWithValue(IDbCommand command, string paramName, DbType type, object value);

        void AddParameterWithValue(IDbCommand command, string paramName, DbType type, ParameterDirection direction,
                                   object value);

        void AddParameter(IDbCommand command, string paramName, DbType type);
        void AddParameter(IDbCommand command, string paramName, DbType type, int size);

        void AddParameter(IDbCommand command, string paramName, DbType type,
                          ParameterDirection direction);

        void AddParameter(IDbCommand command, string paramName, DbType type,
                          ParameterDirection direction, int size);


        void AddParameterArray(IDbCommand command, ParameterDirection direction, string paramName, object parameters,
                               DbType type);

        void AddParameterArray(IDbCommand command, ParameterDirection direction, string paramName, object parameters,
                               DbType type, int size);

        void AddParameterArray(IDbCommand command, ParameterDirection direction, string paramName, DbType type, int size);


        object GetOutputParameterValue(IDbCommand command, string paramName);
        int ExecuteNonQuery(IDbCommand command);
        object ExecuteScalar(IDbCommand command);
    }
}