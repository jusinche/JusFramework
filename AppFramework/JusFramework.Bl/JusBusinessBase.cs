using System;
using System.Data;
using System.Reflection;
using Csla;
using JusFramework.Dal;
using JusFramework.Excepciones;
using MethodInfo = System.Reflection.MethodInfo;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusBusinessBase<T> : BusinessBase<T> where T : JusBusinessBase<T>
    {
        public string GetUsuario
        {
            //Todo:JR. implementar el metodo que me obtenga el usuario concurrente
            get { return "admin"; }
        }

        private int _id;
        /// <summary>
        /// Identificador de la clase
        /// </summary>
        public int Id
        {
            get { return _id; }
            protected set { _id = value; }
        }
        
        private int _version;
        /// <summary>
        /// Version del objeto, Numero de veces que ha sido modificado
        /// </summary>
        public int Version
        {
            get { return _version; }
            protected set { _version= value; }
        }


        public static T New()
        {
            return DataPortal.Create<T>();
        }

        #region Data Access

        protected static readonly string NombreMetodo = "AddParameterCriteria";
        protected abstract string ObtenerSp { get; }
        protected abstract string ActualizarSp { get; }
        protected abstract string InsertarSp { get; }
        protected abstract string BorrarSp { get; }

        [NonSerialized]
        protected  DatabaseConection Db;
        [NonSerialized]
        protected  IDbCommand Comando;
        protected void DataPortal_Fetch(object criteria)
        {
            //crear la conexion a la base
            Db = DatabaseFactory.CreateDatabase();
            Comando = Db.CreateSPCommand(ObtenerSp);

            //setear los parametros
            MethodInfo methodInfo = this.GetType().GetMethod(NombreMetodo, BindingFlags.NonPublic | BindingFlags.Instance,
                Type.DefaultBinder, new[] { criteria.GetType() }, null);
            if (methodInfo != null)
            {
                methodInfo.Invoke(this, new object[] { criteria });
            }
            else
            {
                throw new JusException(String.Format("No se implemento el metodo {0}, Ej: 'private void {0}({1} criteria)'",
                    NombreMetodo, criteria.GetType()));
            }
            
            using (var dr = Db.ExecuteDataReader(Comando))
                
                while (dr.Read())
                {
                    addCommonData(dr);
                    AddObjPost((dr));
                    if (dr.NextResult())
                    {
                        throw  new JusException("Existe mas de un resultado");
                    }
                }
        }

        protected void addCommonData(IDataReader dr)
        {
            Id = Convert.ToInt32(dr["n_id"]);
            Version = Convert.ToInt32(dr["n_VERSION"]);
        }

        protected abstract void AddObjPost(IDataReader data);

        /// <summary>
        /// Agrega los parametros comunes para una sentencia
        /// </summary>
        protected abstract void AddCommonParameters();

        /// <summary>
        /// agrega los parametrso de insert
        /// </summary>
        protected void AddInsertParameters(){}

        /// <summary>
        /// agrega parametros de update
        /// </summary>
        protected void AddUpdateParameters()
        {
            Db.AddParameterWithValue(Comando, "en_id", DbType.Int32, Id);
            Db.AddParameterWithValue(Comando, "ec_usuario", DbType.String, GetUsuario);
            Db.AddParameterWithValue(Comando, "en_version", DbType.Int32, Version);
            Db.AddParameter(Comando, "sn_reg_modificados", DbType.Int32, ParameterDirection.Output);
        }


        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            //crear la conexion a la base
            Db = DatabaseFactory.CreateDatabase();
            Comando = Db.CreateSPCommand(ActualizarSp);

            AddCommonParameters();
            AddUpdateParameters();

            Db.ExecuteNonQuery(Comando);
            
            int resultado;
            int.TryParse(Db.GetOutputParameterValue(Comando, "sn_reg_modificados").ToString(), out resultado);

            if (resultado == 0)
            {
                throw new JusException("No se modifico ningun dato");
            }
            
            
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.Id);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected void DataPortal_Delete(int criteria)
        {
            // TODO: delete values
        }

        #endregion Data Access
    }
}