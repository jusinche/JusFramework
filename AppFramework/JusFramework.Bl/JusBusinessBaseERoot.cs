using System;
using Csla;

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
        

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            base.DataPortal_Insert();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            base.DataPortal_Update();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            base.DataPortal_DeleteSelf();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected new void DataPortal_Delete(int criteria)
        {
            base.DataPortal_Delete(criteria);
        }
    }
}
