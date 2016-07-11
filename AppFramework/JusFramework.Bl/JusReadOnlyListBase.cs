using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    public abstract class JusReadOnlyListBase<T, TCr> : ReadOnlyListBase<T, TCr> where T : JusReadOnlyListBase<T, TCr>
        where TCr : JusReadOnlyBase<TCr>
    {
        protected abstract string NombreProcedimiento { get; }
       
        public TCr GetItem(int id)
        {
            return this.First(x => x.Id == id);
        }

        #region Data Access

        private static readonly string NombreMetodo = "AddParameterCriteria";

        protected abstract Type[] RootClass { get; }

        protected new void DataPortal_Fetch(object criteria)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            GetData(criteria);

            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }

        private int _totalRegistros;
        public int TotalRegistros {
            get { return _totalRegistros; }
        }

        protected void Child_Fetch(object criteria)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            GetData(criteria);

            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
        

        private void GetData(object criteria)
        {
            var key = Getkey(criteria);
            ICache cache = ServiceLocator.Current.GetInstance<ICache>();
            if (cache.Contains(key))
            {
                var itsCache = (IList<TCr>)cache.GetData(key);
                foreach (var cr in itsCache)
                {
                    Items.Add(cr);   
                }
            }
            else
            {
                //crear la conexion a la base
                if (Db == null)
                {
                    Db = DatabaseFactory.CreateDatabase();
                }
                Comando = Db.CreateSPCommand(NombreProcedimiento);

                //setear los parametros
                MethodInfo methodInfo = GetType()
                    .GetMethod(NombreMetodo, BindingFlags.NonPublic | BindingFlags.Instance,
                        Type.DefaultBinder, new[] {criteria.GetType()}, null);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(this, new [] {criteria});
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
                        throw new JusException(
                        String.Format("No se implemento el metodo {0}, Ej: 'private void {0}({1} criteria)'",
                            NombreMetodo, criteria.GetType()));
                    }
                }
                Db.AddParameter(Comando, "sn_total_registros", DbType.Int32, ParameterDirection.Output);
                Db.AddParameter(Comando, "sq_resultado", DbType.Object, ParameterDirection.Output);
                using (IDataReader dr = Db.ExecuteDataReader(Comando))
                {
                    List<TCr> listaInfo = new List<TCr>();
                    Int32.TryParse(Db.GetOutputParameterValue(Comando, "sn_total_registros").ToString(), out _totalRegistros);
                    while (dr.Read() && listaInfo.Count<500)
                    {
                        listaInfo.Add(JusReadOnlyBase<TCr>.Get(dr));
                    }
                    foreach (var info in OrdenarList(listaInfo))
                    {
                        Add(info);
                    }
                }
                if (Items.Count > 1)
                {
                    var grupo = RootClass.Aggregate(string.Empty, (current, type) => current + type + ',');
                    cache.AddItem(key, Items, grupo.Trim(','));
                }
            }
        }
        protected abstract List<TCr> OrdenarList(List<TCr> lista);



        private string Getkey(object criteria)
        {
            if (criteria is ICacheable)
            {
                return ((ICacheable) criteria).GetKey();
            }
            return string.Format("{0}_{1}", GetType(), criteria);
        }

        public static T Get(object criteria)
        {
            return DataPortal.FetchChild<T>(criteria);
        }

        public static T Get(string criteria)
        {
            return DataPortal.FetchChild<T>(criteria);
        }

        public static T Get(int criteria)
        {
            return DataPortal.FetchChild<T>(criteria);
        }

        protected DatabaseConection Db;
        protected IDbCommand Comando;

        #endregion

    }
}