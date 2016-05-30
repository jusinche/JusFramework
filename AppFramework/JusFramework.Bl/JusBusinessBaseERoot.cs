using System;
using Csla;
using JusFramework.Dal;
using JusFramework.Excepciones;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusBusinessBaseERoot<T> : JusBusinessBase<T> where T : JusBusinessBaseERoot<T>
    {
        protected static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
          //  BusinessRules.AddRule(typeof(T), (new AuthorizationObtener(AuthorizationActions.CreateObject)));

        }
        
        public static T New()
        {
            return DataPortal.Create<T>();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            // TODO: insert values
            //crear la conexion a la base
            Db = DatabaseFactory.CreateDatabase();
            Comando = Db.CreateSPCommand(ObtenerSp);

            AddInsertParameters();
            AddCommonParameters();


            using (var dr = Db.ExecuteDataReader(Comando))
                while (dr.Read())
                {
                    AddObjPost((dr));
                    if (dr.NextResult())
                    {
                        throw new JusException("Existe mas de un resultado");
                    }
                }
        }
    }
}
