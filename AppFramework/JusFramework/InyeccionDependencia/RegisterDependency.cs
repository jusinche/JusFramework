using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace JusFramework.InyeccionDependencia
{
    public class RegisterDependency
    {
        private RegisterDependency()
        {
            
        }

        public static void Init()
        {
            new RegisterDependency().Inicializacion();
        }

        private void Inicializacion()
        {
            ServiceLocator.SetLocatorProvider(() => new ServiceLocatorStructureMap());

            ObjectFactory.Initialize(x =>
            {
                x.PullConfigurationFromAppConfig = true;
            });
        }

    }
}
