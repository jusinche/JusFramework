using System;
using JusFramework.Encriptacion;
using JusFramework.InyeccionDependencia;
using Microsoft.Practices.ServiceLocation;

namespace JusConsole.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            RegisterDependency.Init();
            IServiceLocator locator = ServiceLocator.Current;
            IEncripta e = locator.GetInstance<IEncripta>();
            Console.Read();

            
        }
    }
}
