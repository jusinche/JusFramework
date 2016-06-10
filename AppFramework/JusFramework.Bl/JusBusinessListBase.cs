using System;
using System.Data;
using Csla;
using JusFramework.Dal;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusBusinessListBase <T, TS> :BusinessListBase<T, TS> 
        where T : JusBusinessListBase<T,TS>
        where TS : JusBusinessBaseEChild<TS>
    {
        #region Authorization Rules


        #endregion

        #region Factory Methods

        public static T New()
        {
            return DataPortal.Create<T>();
        }

        public static T Get(int id)
        {
            return DataPortal.Fetch<T>(id);
        }


        #endregion

        #region Data Access

        [NonSerialized]
        protected DatabaseConection Db;
        [NonSerialized]
        protected IDbCommand Comando;

        protected abstract string NombreProcedimiento { get; }

        protected void DataPortal_Fetch(int id)
        {
            RaiseListChangedEvents = false;
            
            //crear la conexion a la base
            if (Db == null)
            {
                Db = DatabaseFactory.CreateDatabase();
            }
            Comando = Db.CreateSPCommand(NombreProcedimiento);
            Db.AddParameterWithValue(Comando, "en_padre", DbType.Int32, id);
            Db.AddParameter(Comando, "sq_resultado", DbType.Object, ParameterDirection.Output);

            using (IDataReader dr = Db.ExecuteDataReader(Comando))
            {
                while (dr.Read())
                {
                    Add(JusBusinessBaseEChild<TS>.Get(dr));
                }
            }
            RaiseListChangedEvents = true;
        }

        protected override void DataPortal_Update()
        {
            // TODO: open database, update values
            //base.Child_Update();
        }

        #endregion
    }
}
