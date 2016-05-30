using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Csla;
using Csla.Core;
using Csla.Rules;
using Csla.Server;
using JusFramework.InyeccionDependencia.Mapper;
using DataPortal = Csla.DataPortal;

//sing DataPortal = Csla.DataPortal;

namespace JusFramework.Bl
{
    public class JusObjectFactory
        : Csla.Server.ObjectFactory, Csla.Server.IObjectFactoryLoader
    {
        public object Create()
        {
            return null;
        }
        public object Fetch(SingleCriteria<BusinessBase, int> criteria)
        {
            return null;
        }
        public object Update(object obj)
        {
            //this.Update(obj);
            return null;
        }
        public void Delete(SingleCriteria<BusinessBase, int> criteria)
        {
            
        }

        public Type GetFactoryType(string factoryName)
        {
            return null;
            //return 

        }

        public object GetFactory(string factoryName)
        {
            return null;
        }
    }
}