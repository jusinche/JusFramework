using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using JusFramework;
using JusFramework.Bl;
using JusFramework.Cache;

namespace JusNucleo.Bl.Personas
{
    [Serializable]
    public class PersonaNaturalCriteria : JusCriteriaBase<PersonaNaturalCriteria>, ICacheable
    {
        #region Business Methods



        #endregion
        private PersonaNaturalCriteria()
        { /* Require use of factory methods */ }


        public string GetKey()
        {
            //return string.Format("{0}_{1}_{2}", GetType(), SistemaCodigo, Usuario);
            return null;
        }
    }
}
