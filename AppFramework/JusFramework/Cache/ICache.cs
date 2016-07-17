using System.Collections.Generic;

namespace JusFramework.Cache
{
    public interface ICache
    {
        /// <summary>
        /// Agrega un nuevo item de cachel
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="group"></param>
        void AddItem(string key, object data, string group = null);


        /// <summary>
        /// Remueve un item de cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="group"></param>
        void RemoveItem(string key, string group = null);

        /// <summary>
        /// Elimina una lista de items de caches
        /// </summary>
        /// <param name="keys"></param>
        void RemoveItems(IEnumerable<string> keys);

        /// <summary>
        /// Verifica si existe un item de cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        bool Contains(string key, string group = null);

        /// <summary>
        /// Limpia elimina(limpia) la cache
        /// </summary>
        /// <param name="group"></param>
        void Clear(string group = null);

        /// <summary>
        /// Obitiene la un objeto que ha sido chacheado
        /// </summary>
        /// <param name="key"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        object GetData(string key, string group = null);

        /// <summary>
        /// Obtiene el total de items que se encuentran cacheados
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        int GetTotalOfItems(string group = null);
    }
}
