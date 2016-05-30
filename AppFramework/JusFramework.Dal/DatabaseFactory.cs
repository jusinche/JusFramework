// -------------------------------------------
// DatabaseFactory
//
// Código tomado de: http://www.primaryobjects.com/CMS/Article81.aspx
// 
// Modificado por: Edison Torres 
// Fecha: 3/12/2009
// Descripción: Provee una fabrica de objetos para crear tipos de bases de datos
// 
//--------------------------------------------

using System;
using System.Configuration;
using System.Reflection;

namespace JusFramework.Dal
{
    ///<summary>
    /// Provee una fabrica de objetos para crear tipos de bases de datos
    ///</summary>
    public static class DatabaseFactory
    {
        static DatabaseConfiguration sectionHandler =
            (DatabaseConfiguration) ConfigurationManager.GetSection("DatabaseFactoryConfiguration");


        /// <summary>
        /// Variable paraa realizar Log
        /// </summary>
        //private static readonly ILogger log =
        //    ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(DatabaseFactory));
        /// <summary>
        /// Devuelve la instancia de implementación de base de datos especificada por defecto.
        /// </summary>
        /// <returns></returns>
        public static DatabaseConection CreateDatabase()
        {
            return CreateDatabase("");
        }

        /// <summary>
        /// Devuelve la instancia de implementación de base de datos según su nombre en la configuración.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DatabaseConection CreateDatabase(string name)
        {
            int count = 0;
            int indice = -1;

            if (sectionHandler == null)
                throw new Exception(
                    "There is not a DatabaseFactoryConfiguration section on configuration application file.");

            if (sectionHandler.Name.Length == 0)
                throw new Exception(
                    "There is not a DatabaseFactoryConfiguration section on configuration application file.");

            if (sectionHandler.Databases.Count == 0)
                throw new Exception("There is not a default database.");

            if (string.IsNullOrEmpty(name))
            {
                for (int i = 0; i < sectionHandler.Databases.Count; i++)
                {
                    if (sectionHandler.Databases[i].IsDefaultDatabase)
                    {
                        count++;
                        indice = i;
                    }
                }

                if (count == 0)
                    throw new Exception("There is no one database marked as default");

                if (count > 1)
                    throw new Exception("There is more than one database marked as default");

                return CreateDatabaseInternal(sectionHandler.Databases[indice].Type,
                                              sectionHandler.Databases[indice].ConnectionString);
            }


            for (int i = 0; i < sectionHandler.Databases.Count; i++)
            {
                if (sectionHandler.Databases[i].Name == name)
                {
                    count++;
                    indice = i;
                }
            }

            if (count > 1)
                throw new Exception(string.Format("There is more than one database with the name '{0}'", name));

            if (indice == -1)
                throw new Exception(string.Format("There is no database element with the name '{0}'", name));

            return CreateDatabaseInternal(sectionHandler.Databases[indice].Type,
                                          sectionHandler.Databases[indice].ConnectionString);
        }

        /// <summary>
        /// Método interno para crear mediante reflexión la instancia específica de base de datos.
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static DatabaseConection CreateDatabaseInternal(string typeName, string connectionString)
        {
            try
            {
                Type database = Type.GetType(typeName);
                ConstructorInfo constructor = database.GetConstructor(new[] {typeof (string)});
                DatabaseConection createdObject = (DatabaseConection)constructor.Invoke(new object[] { connectionString });

                return createdObject;
            }
            catch (Exception excep)
            {
                throw new Exception(
                    string.Format("Error instantiating database '{0}', Error '{1} {2} Inner Exception: {3}'",
                                  typeName,
                                  excep.Message, excep.StackTrace,
                                  GetInnerExceptionsMessages(excep.InnerException)));
            }
        }

        static string GetInnerExceptionsMessages(Exception innerException)
        {
            if (innerException == null) return "";
            return innerException.Message + " " + GetInnerExceptionsMessages(innerException.InnerException);
        }
    }
}