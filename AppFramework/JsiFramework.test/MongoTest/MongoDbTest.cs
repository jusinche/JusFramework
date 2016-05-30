using System;
using JusFramework.Cache;
using JusFramework.Dal.JusMongoDB;
using JusFramework.InyeccionDependencia;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;


namespace JusFramework.test.MongoTest
{

    [TestClass]
    public class MongoDbTest
    {
        private string key = "prueba2";
        [TestMethod]
        public void InsertMongoInfo()
        {
            RegisterDependency.Init();

            ICache mongoDb = ServiceLocator.Current.GetInstance<ICache>();
            var data =new ObjTest();
            data.Clave = "clave";
            data.Valor = "Vaqlor";

            mongoDb.AddItem(key, data,"");
            var data1=(ObjTest)(mongoDb.GetData(key));
            Assert.AreEqual(data1.Valor, data.Valor);

        }
        [TestMethod]
        public void GetMongoInfo()
        {
            InsertMongoInfo();

            ICache mongoDb = ServiceLocator.Current.GetInstance<ICache>();
            
            var data1 = (ObjTest)(mongoDb.GetData(key));
            Assert.AreEqual(data1.Clave, "clave");

        }

        [TestMethod]
        public void RemoveMongoInfo()
        {
            RegisterDependency.Init();

            ICache mongoDb = ServiceLocator.Current.GetInstance<ICache>();

            mongoDb.RemoveItem(key);

            var data1 = (ObjTest)(mongoDb.GetData(key));

            Assert.IsNull(data1);

        }
    }

    [Serializable]
    public class ObjTest
    {
        public string Clave { get; set; }
    
        public string Valor { get; set; }

    }
}
