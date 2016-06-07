using System;
using Csla;
using JusFramework.Cache;
using Microsoft.Practices.ServiceLocation;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusBusinessBaseERoot<T> : JusBusinessBase<T> where T : JusBusinessBaseERoot<T>
    {
        [NonSerialized]
        private ICache _cache = ServiceLocator.Current.GetInstance<ICache>();

        protected static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
          //  BusinessRules.AddRule(typeof(T), (new AuthorizationObtener(AuthorizationActions.CreateObject)));
        }
        

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            base.DataPortal_Insert();
            if (_cache == null)
            {
                _cache = ServiceLocator.Current.GetInstance<ICache>();
            }
            _cache.Clear(typeof(T).ToString());
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            base.DataPortal_Update();
            if (_cache == null)
            {
                _cache = ServiceLocator.Current.GetInstance<ICache>();
            }
            _cache.Clear(typeof(T).ToString());
        }
        

        [Transactional(TransactionalTypes.TransactionScope)]
        protected new void DataPortal_Delete(int criteria)
        {
            base.DataPortal_Delete(criteria);
            if (_cache == null)
            {
                _cache = ServiceLocator.Current.GetInstance<ICache>();
            }
            _cache.Clear(typeof(T).ToString());
        }
    }
}
