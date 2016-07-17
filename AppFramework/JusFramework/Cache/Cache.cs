using System.Collections.Generic;

namespace JusFramework.Cache
{
    public class Cache: ICache
    {

        public void AddItem(string key, object data, string @group = null)
        {
        }


        public void RemoveItem(string key, string @group = null)
        {
            
        }

        public void RemoveItems(IEnumerable<string> keys)
        {
            
        }

        public bool Contains(string key, string @group = null)
        {
            return false;
        }

        public void Clear(string @group = null)
        {
            
        }

        public object GetData(string key, string @group = null)
        {
            return null;
        }

        public int GetTotalOfItems(string @group = null)
        {
            return 0;
        }
    }
}
