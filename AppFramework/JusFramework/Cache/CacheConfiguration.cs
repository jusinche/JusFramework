using System.Configuration;

namespace JusFramework.Cache
{
    public class CacheConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return (string)base["name"]; }
        }

        [ConfigurationProperty("CacheItems",IsDefaultCollection = true)]
        public CacheCollection CacheItems
        {
            get
            {
                CacheCollection cacheItems = (CacheCollection)base["CacheItems"];
                return cacheItems;
            }
        }
    }

    public class CacheCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CacheElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CacheElement)element).Key;
        }
    }
    public class CacheElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public string Key { get; set; }

        [ConfigurationProperty("name", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public string EditableObject { get; set; }


        [ConfigurationProperty("name", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public string Group { get; set; }


        [ConfigurationProperty("name", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public string  ReadOnlyList{ get; set; }
    }
}
