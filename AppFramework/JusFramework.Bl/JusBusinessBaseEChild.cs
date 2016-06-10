using System;
using System.Data;
using Csla;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusBusinessBaseEChild<T> : JusBusinessBase<T> where T : JusBusinessBaseEChild<T>
    {
        public new static T New()
        {
            return DataPortal.CreateChild<T>();
        }

        public new static T Get(object childData)
        {
            return DataPortal.FetchChild<T>(childData);
        }

        protected void Child_Insert()
        {
            
            DataPortal_Insert();
        }

       
        protected void Child_Update()
        {
            DataPortal_Update();
        }

        protected void Child_DeleteSelf()
        {
            DataPortal_DeleteSelf();
        }

        protected void Child_Fetch(IDataReader dr)
        {
            AddCommonData(dr);
            Fetch(dr);
        }

        
    }
}
