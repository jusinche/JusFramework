

using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;

namespace JusFramework.InyeccionDependencia.Mapper
{
    /// <summary>
    /// Clase para realizar creación de objetos con reflexión con un mejor performance
    /// </summary>
    public static class JusObjectFactory
    {
        #region Delegates

        public delegate object CreateObject();

        #endregion

        static readonly Hashtable CreatorCache = Hashtable.Synchronized(new Hashtable());
        static readonly Type CoType = typeof (CreateObject);

        /// <summary>
        /// Clear cache
        /// </summary>
        public static void Clear()
        {
            lock (CreatorCache.SyncRoot)
            {
                CreatorCache.Clear();
            }
        }


        /// <summary>
        /// Create a new instance of the specified type
        /// </summary>
        /// <returns></returns>
        public static CreateObject CreateObjectFactory<T>() // where T : class
        {
            Type t = typeof (T);
            CreateObject c = CreatorCache[t] as CreateObject;
            if (c == null)
            {
                lock (CreatorCache.SyncRoot)
                {
                    c = CreatorCache[t] as CreateObject;
                    if (c != null)
                    {
                        return c;
                    }
                    DynamicMethod dynMethod = new DynamicMethod("DM$OBJ_FACTORY_" + t.Name, typeof (object), null, t);
                    ILGenerator ilGen = dynMethod.GetILGenerator();

                    //ilGen.Emit(OpCodes.Newobj, t.GetConstructor(Type.EmptyTypes));
                    ilGen.Emit(OpCodes.Newobj, t.GetConstructor(BindingFlags.Public |
                                                                BindingFlags.NonPublic | BindingFlags.Instance, null,
                                                                Type.EmptyTypes, null));

                    ilGen.Emit(OpCodes.Ret);
                    c = (CreateObject) dynMethod.CreateDelegate(CoType);
                    CreatorCache.Add(t, c);
                }
            }
            return c;
        }
    }
}