using System;
using System.Collections.Generic;
using Csla;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusBusinessListBase <T, TS> :BusinessListBase<T, TS> 
        where T : JusBusinessListBase<T,TS>
        where TS : JusBusinessBaseEChild<TS>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(EditableRootList1), "Role");
        }

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

        protected void DataPortal_Fetch(int criteria)
        {
            RaiseListChangedEvents = false;
            // TODO: load values into memory
            object childData = null;
            foreach (var item in (List<object>)childData)
                this.Add(JusBusinessBaseEChild<TS>.Get(childData));
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
