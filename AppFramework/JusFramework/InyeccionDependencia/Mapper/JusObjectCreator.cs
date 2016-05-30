namespace JusFramework.InyeccionDependencia.Mapper
{

    public interface IJusObjectCreator
    {
        /// <summary>
        /// Crear una instancia de objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateInstance<T>();
    }

    public class JusObjectCreator : IJusObjectCreator
    {
        public T CreateInstance<T>()
        {
            var creator = JusObjectFactory.CreateObjectFactory<T>();
            var obj = creator() is T ? (T) creator() : default(T);
            return obj;
        }
    }
}
