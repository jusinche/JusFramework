using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Csla;
using JusFramework.Cache;
using JusFramework.Dal;
using JusFramework.Excepciones;
using Microsoft.Practices.ServiceLocation;
using MethodInfo = System.Reflection.MethodInfo;

namespace JusFramework.Bl
{
    [Serializable]
    [ClassRoot]
    public abstract class JusReadOnlyListBase<T, TCr> : ReadOnlyListBase<T, TCr> where T : JusReadOnlyListBase<T, TCr>
        where TCr : JusReadOnlyBase<TCr>
    {
        protected abstract string NombreProcedimiento { get; }

        #region Data Access

        private static readonly string NombreMetodo = "AddParameterCriteria";

        protected void DataPortal_Fetch(object criteria)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            GetData(criteria);

            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }


        //public new void Add(TCr item)
        //{
        //    base.Add(item);
        //}

        private void GetData(object criteria)
        {
            var key = Getkey(criteria);
            ICache cache = ServiceLocator.Current.GetInstance<ICache>();
            if (cache.Contains(key))
            {
                // dr = cache.GetData(key);
                var itsCache = (IList<TCr>)cache.GetData(key);
                foreach (var cr in itsCache)
                {
                    Items.Add(cr);   
                }
            }
            else
            {
                //crear la conexion a la base
                Db = DatabaseFactory.CreateDatabase();
                Comando = Db.CreateSPCommand(NombreProcedimiento);

                //setear los parametros
                MethodInfo methodInfo = this.GetType()
                    .GetMethod(NombreMetodo, BindingFlags.NonPublic | BindingFlags.Instance,
                        Type.DefaultBinder, new[] {criteria.GetType()}, null);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(this, new object[] {criteria});
                }
                else
                {
                    throw new JusException(
                        String.Format("No se implemento el metodo {0}, Ej: 'private void {0}({1} criteria)'",
                            NombreMetodo, criteria.GetType()));
                }
                using (IDataReader dr = Db.ExecuteDataReader(Comando))
                {

                    while (dr.Read())
                    {
                        Add(JusReadOnlyBase<TCr>.Get(dr));
                    }
                }
                cache.AddItem(key, Items);
            }

        }

        private string Getkey(object criteria)
        {
            if (criteria is ICacheable)
            {
                return ((ICacheable) criteria).GetKey();
            }
            //if (criteria.GetType().IsPrimitive)
            //return string.Format("{0}_{1}", GetType(),criteria);
            return string.Format("{0}_{1}", GetType(), criteria);
        }

        public static T Get(object criteria)
        {
            return DataPortal.FetchChild<T>(criteria);
        }

        protected DatabaseConection Db;
        protected IDbCommand Comando;

        #endregion

    }
}