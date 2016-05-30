// -------------------------------------------
// DatabaseConfigurationSectionHandler
//
// Código basado en: http://www.primaryobjects.com/CMS/Article81.aspx
// 
// Modificado por: Edison Torres 
// Fecha: 3/12/2009
// Descripción: Representa una sección en un archivo de configuración
// 
//--------------------------------------------

using System;
using System.Configuration;

namespace JusFramework.Dal
{
    ///<summary>
    /// Representa una sección en un archivo de configuración
    ///</summary>
    public sealed class DatabaseConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return (string) base["name"]; }
        }

        [ConfigurationProperty("databases",
            IsDefaultCollection = true)]
        public DatabaseElementCollection Databases
        {
            get
            {
                DatabaseElementCollection databaseCollection = (DatabaseElementCollection) base["databases"];
                return databaseCollection;
            }
        }
    }

    public class DatabaseElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public String Name
        {
            get { return (String) this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public string Type
        {
            get { return (string) this["type"]; }
        }


        [ConfigurationProperty("isDefaultDatabase", DefaultValue = false)]
        public bool IsDefaultDatabase
        {
            get { return (bool) this["isDefaultDatabase"]; }
        }

        [ConfigurationProperty("connectionStringName")]
        public string ConnectionStringName
        {
            get { return (string) base["connectionStringName"]; }
        }

        public string ConnectionString
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
                }
                catch (Exception excep)
                {
                    throw new Exception("Connection string " + ConnectionStringName + " was not found in web.config. " +
                                        excep.Message);
                }
            }
        }
    }

    public class DatabaseElementCollection : ConfigurationElementCollection
    {
        public DatabaseElement this[int index]
        {
            get { return (DatabaseElement) BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new string AddElementName
        {
            get { return base.AddElementName; }
            set { base.AddElementName = value; }
        }

        public new string ClearElementName
        {
            get { return base.ClearElementName; }
            set { base.AddElementName = value; }
        }

        public new string RemoveElementName
        {
            get { return base.RemoveElementName; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseElement) element).Name;
        }
    }
}