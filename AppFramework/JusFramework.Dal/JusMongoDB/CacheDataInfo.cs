using System;

namespace JusFramework.Dal.JusMongoDB
{
    public class CacheDataInfo
    {
        /// <summary>
        /// Identificador del cache
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Clave de la cache
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Grupo al que pertenece
        /// </summary>
        public string Gruop { get; set; }

        /// <summary>
        /// Clase editable
        /// </summary>
        public string EditableClass { get; set; }

        /// <summary>
        /// Fecha de creacion
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Informacion de la lista
        /// </summary>
        public string Data { get; set; }
    }
}
