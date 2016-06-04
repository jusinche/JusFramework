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


        public static T Get(int id)
        {
            return DataPortal.Fetch<T>(id);
        }

        #region Data Access

        private static readonly string _nombreMetodo = "AddParameterCriteria";
        protected abstract string ObtenerSp { get; }
        protected abstract string ActualizarSp { get; }
        protected abstract string InsertarSp { get; }
        protected abstract string BorrarSp { get; }

        protected static string NombreMetodo
        {
            get { return _nombreMetodo; }
        }


        //private void DataPortal_Fetch(int criteria)
        //{
        //    // TODO: load values
        //}

        [NonSerialized]
        protected  DatabaseConection Db;
        [NonSerialized]
        protected  IDbCommand Comando;
        protected void DataPortal_Fetch(object criteria)
        {
            //crear la conexion a la base
            if (Db == null)
            {
                Db = DatabaseFactory.CreateDatabase();
            }
            Comando = Db.CreateSPCommand(ObtenerSp);

            //setear los parametros
            MethodInfo methodInfo = GetType().GetMethod(_nombreMetodo, BindingFlags.NonPublic | BindingFlags.Instance,
                Type.DefaultBinder, new[] { criteria.GetType() }, null);
            if (methodInfo != null)
            {
                methodInfo.Invoke(this, new [] { criteria });
            }
            else
            {
                if (criteria is int)
                {
                    Db.AddParameterWithValue(Comando, "en_id", DbType.Int32, criteria);
                }
                else if (criteria is string)
                {
                    
                    Db.AddParameterWithValue(Comando, "ec_codigo", DbType.String, criteria);
                }
                else
                {
                    throw new JusException(String.Format("No se implemento el metodo {0}, Ej: 'private void {0}({1} criteria)'",
                    _nombreMetodo, criteria.GetType()));
                }
            }
            Db.AddParameter(Comando, "sq_resultado", DbType.Object, ParameterDirection.Output);
            using (var dr = Db.ExecuteDataReader(Comando))
                
                while (dr.Read())
                {
                    AddCommonData(dr);
                    Fetch(dr);
                    if (dr.NextResult())
                    {
                        throw  new JusException("Existe mas de un resultado");
                    }
                }
        }

        protected void AddCommonData(IDataReader dr)
        {
            Id = Convert.ToInt32(dr["n_id"]);
            Version = Convert.ToInt32(dr["n_version"]);
        }


        /// <summary>
        /// Agrega los parametros comunes para una sentencia
        /// </summary>
        protected abstract void AddCommonParameters();

        /// <summary>
        /// agrega los parametrso de insert
        /// </summary>
        protected virtual void AddInsertParameters()
        {
            Db.AddParameterWithValue(Comando, "ec_usuario", DbType.String, GetUsuario);
            Db.AddParameter(Comando, "sn_id", DbType.Int32, ParameterDirection.Output);
        }

        /// <summary>
        /// agrega parametros de update
        /// </summary>
        protected virtual void AddUpdateParameters()
        {
            Db.AddParameterWithValue(Comando, "en_id", DbType.Int32, Id);
            Db.AddParameterWithValue(Comando, "ec_usuario", DbType.String, GetUsuario);
            Db.AddParameterWithValue(Comando, "en_version", DbType.Int32, Version);
            Db.AddParameter(Comando, "sn_reg_modificados", DbType.Int32, ParameterDirection.Output);
        }

        protected override void DataPortal_Insert()
        {
            //crear la conexion a la base
            if (Db == null)
            {
                Db = DatabaseFactory.CreateDatabase();
            }
            Comando = Db.CreateSPCommand(InsertarSp);

            AddCommonParameters();
            AddInsertParameters();

            Db.ExecuteNonQuery(Comando);

            int resultado;
            int.TryParse(Db.GetOutputParameterValue(Comando, "sn_id").ToString(), out resultado);
            Id = resultado;

            if (resultado == 0)
            {
                throw new JusException("No se Inserto ningun dato");
            }
            PostInsert();
        }


        protected virtual void PostInsert()
        {
        }

        protected virtual void PostUpdate()
        {
        }

        protected abstract void Fetch(IDataReader dr);

        protected override void DataPortal_Update()
        {
            //crear la conexion a la base
            if (Db ==null)
            {
                Db = DatabaseFactory.CreateDatabase();   
            }
            
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
            PostUpdate();
        }

        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(Id);
        }

        protected void DataPortal_Delete(int criteria)
        {

            //crear la conexion a la base
            if (Db == null)
            {
                Db = DatabaseFactory.CreateDatabase();
            }

            Comando = Db.CreateSPCommand(BorrarSp);


            Db.AddParameterWithValue(Comando, "en_id", DbType.Int32, Id);
            Db.AddParameterWithValue(Comando, "en_version", DbType.Int32, Version);
            Db.AddParameter(Comando, "sn_reg_modificados", DbType.Int32, ParameterDirection.Output);

            Db.ExecuteNonQuery(Comando);

            int resultado;
            int.TryParse(Db.GetOutputParameterValue(Comando, "sn_reg_modificados").ToString(), out resultado);

            if (resultado == 0)
            {
                throw new JusException("No se modifico ningun dato");
            }
        }



        #endregion Data Access
    }
}