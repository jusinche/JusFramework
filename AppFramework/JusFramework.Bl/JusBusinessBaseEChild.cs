using System;
using Csla;

namespace JusFramework.Bl
{
    [Serializable]
    public abstract class JusBusinessBaseEChild<T> : JusBusinessBase<T> where T : JusBusinessBaseEChild<T>
    {
        internal static T New()
        {
            return DataPortal.CreateChild<T>();
        }

        public static T Get(object childData)
        {
            return DataPortal.FetchChild<T>(childData);
        }
        
    }
}
