using System;
using System.Data;
using JusFramework.Bl;

namespace JusNucleo.Bl.Comun
{
    [Serializable]
    public class CatalogoItemList :
      JusReadOnlyListBase<CatalogoItemList, CatalogoItemInfo>
    {

        #region Factory Methods

        //public static CatalogoItemList Get(string criteria)
        //{
        //    return DataPortal.Fetch<CatalogoItemList>(criteria);
        //}

        private CatalogoItemList()
        { /* require use of factory methods */ }

        #endregion

        protected override string NombreProcedimiento
        {
            get { return ProcedimientosConstantes.PrcItemCataloObt; }
        }
       
    }

    [Serializable]
    public class CatalogoItemInfo : JusReadOnlyBase<CatalogoItemInfo>
    {
        #region Business Methods

        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public string Catalogo { get; private set; }
        public string CatalogoNombre { get; private set; }

        #endregion


        #region Factory Methods



        private CatalogoItemInfo()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void Child_Fetch(IDataReader data)
        {
            Id = Convert.ToInt32(data["ite_id"]);
            Nombre = data["ite_nombre"].ToString();
            Codigo = data["ite_codigo"].ToString();
            Catalogo = data["cat_codigo"].ToString();
            CatalogoNombre = data["cat_nombre"].ToString();
        }

        #endregion
    }
}
