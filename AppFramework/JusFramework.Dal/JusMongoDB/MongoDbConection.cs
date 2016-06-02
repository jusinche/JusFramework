﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using JusFramework.Cache;
using MongoDB.Driver;

namespace JusFramework.Dal.JusMongoDB
{
    public class MongoDbConection: ICache
    {
        private string ConnectionString { get; set; }
    
        private  string DatabaseName{ get; set; }

        private IMongoDatabase _mongoServer;
        private IMongoCollection<CacheDataInfo> _mongoCollection;
        public MongoDbConection(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            var cliente = new MongoClient(ConnectionString);
            //// Get a reference to a server object from the Mongo client object
            _mongoServer = cliente.GetDatabase(DatabaseName);
            _mongoCollection = _mongoServer.GetCollection<CacheDataInfo>(typeof(CacheDataInfo).Name.ToLower() + "s");
        }


        public void AddItem(string key, object data, string group = null)
        {
           

            var info= new CacheDataInfo();
            info.CreateDate=DateTime.Now;
            info.Key = key;
            info.Data = SerializeObject(data);
            info.Gruop = group;
            info.ObjectList = data.GetType().FullName;
            info.Id = Guid.NewGuid();

            
             _mongoCollection.InsertOne(info);

        }

        public void RemoveItem(string key, string group = null)
        {
            _mongoCollection.DeleteOne(x => x.Key == key);
        }

        public void RemoveItems(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                RemoveItem(key);
            }
        }

        public bool Contains(string key, string @group = null)
        {
            var cache = _mongoCollection.Find(x => x.Key == key).ToList();

            if (cache.Count==0)
            {
                return false;
            }
            var item = cache.First();
            if (item.CreateDate.Date.Ticks != DateTime.Now.Date.Ticks)
            {
                //borrar cache
                _mongoCollection.DeleteMany(x => x.ObjectList == item.ObjectList);
                return false;
            }
            return cache.Count!=0;
        }

        public void Clear(string @group = null)
        {
            throw new NotImplementedException();
        }

        public  object GetData(string key, string @group = null)
        {
            var lista = _mongoCollection.Find(x => x.Key == key).ToList();

            foreach (CacheDataInfo info in lista)
            {
                return DeserializeObject(info.Data);
            }
            return null ;
        }

        public int GetTotalOfItems(string @group = null)
        {
            return _mongoCollection.Find(x=>x.Key !=String.Empty).ToList().Count;
        }

         

        


        private string SerializeObject(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            var strData = string.Empty;

            foreach (var b in ms.ToArray())
            {
                strData = strData + (char)b;
            }
            return ToHexa( strData);
        }

        // Convert a byte array to an Object
        private Object DeserializeObject(string data)
        {
            data = ToStr(data);
            var buffer=new List<byte>();
            foreach (var c in data)
            {
                buffer.Add((byte)c);
            }

            byte[] arrBytes = buffer.ToArray();


            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            return binForm.Deserialize(memStream);

        }

        private string ToHexa(string input)
        {
            char[] values = input.ToCharArray();
            string hexOutput = string.Empty;
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                 hexOutput =hexOutput+ String.Format("{0:X}", value)+" ";
            }
            return hexOutput.Trim();
        }

        private string ToStr(string hexValues)
        {
            string[] hexValuesSplit = hexValues.Split(' ');
            string strRtr = string.Empty;
            foreach (String hex in hexValuesSplit)
            {
                // Convert the number expressed in base-16 to an integer.
                int value = Convert.ToInt32(hex, 16);
                // Get the character corresponding to the integral value.
                string stringValue = Char.ConvertFromUtf32(value);
                strRtr = strRtr + stringValue;
            }
            return strRtr;
        }
    }
}
