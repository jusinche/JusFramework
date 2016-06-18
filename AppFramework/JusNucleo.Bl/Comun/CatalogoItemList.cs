using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using JusFramework.Bl;

namespace JusNucleo.Bl.Comun
{
    [Serializable]
    public class CatalogoItemList :
      JusReadOnlyListBase<CatalogoItemList, CatalogoItemInfo>
    {

        #region Factory Methods

        
        private CatalogoItemList()
        { /* require use of factory methods */ }

        public CatalogoItemInfo GetItem(string codigoItem)
        {
            return this.First(x => x.Codigo == codigoItem);
        }

        #endregion

        protected override string NombreProcedimiento
        {
            get { return ProcedimientosConstantes.PrcItemCataloObt; }
        }

        protected override Type[] RootClass
        {
            get { return new Type[0];}
        }

        protected override List<CatalogoItemInfo> OrdenarList(List<CatalogoItemInfo> lista)
        {
            return lista.OrderBy(x=>x.Nombre).ToList();
        }

        public List<SelectListItem> ListItems
        {
            get
            {
                return Items.Select(f => new SelectListItem
                                              {
                                                  Value = f.Id.ToString(),
                                                  Text = f.Nombre
                                              }).ToList();
            }
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

        protected void Child_Fetch(IDataReader data)
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
