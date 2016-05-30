using System;
using System.Data;
using Csla;

namespace JusFramework.Bl
{
[Serializable]
    public abstract class JusReadOnlyBase<T> : ReadOnlyBase<T> where T : JusReadOnlyBase<T>
    {
        public int Id
        {
            get;
            protected set; 
        }

        public static T New()
        {
            return DataPortal.CreateChild<T>();
        }

        public static T Get(IDataReader childData)
        {
            return DataPortal.FetchChild<T>(childData);
        }

        public static T Get()
        {
            return DataPortal.Fetch<T>();
        }
    }
}
