using System;
using Csla;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusBusinessBaseEChild<T> : JusBusinessBase<T> where T : JusBusinessBaseEChild<T>
    {
        public static T New()
        {
            return DataPortal.CreateChild<T>();
        }

        public static T Get(object childData)
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

        protected void Child_Delete()
        {
            DataPortal_DeleteSelf();
        }
    }
}
