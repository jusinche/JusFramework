using System;
using Csla;
using JusFramework.Cache;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusCriteriaBase<T> : BusinessBase<T> where T : JusCriteriaBase<T>, ICacheable
    {
       

        #region Factory Methods

        public static T New()
        {
            return DataPortal.Create<T>();
        }

        protected JusCriteriaBase()
        { /* Require use of factory methods */ }
        

        #endregion
    }
}
