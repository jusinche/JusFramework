using System.Security.Claims;
using System.Security.Principal;
using JusNucleo.Bl.Sistema.Logeo;

namespace JusNucleo.Bl.Sistema.Aplicacion
{
    public class JusIdentity : IIdentity

    {
        public string Name {
            get { return _cuenta.Login; }
        }
        public string AuthenticationType {
            get {return ClaimsIdentity.DefaultIssuer; }
        }
        public bool IsAuthenticated {
            get {
                if (_cuenta!=null)
                {
                    return true;
                }
                return false;
            }
        }

        private Cuenta _cuenta;

        public void SetCuenta(Cuenta cuenta)
        {
            if (cuenta!=null)
            {
                _cuenta = cuenta;
            }
            
        }
    }
}