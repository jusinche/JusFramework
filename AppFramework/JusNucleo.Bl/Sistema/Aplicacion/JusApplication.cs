using System.Security.Principal;

namespace JusNucleo.Bl.Sistema.Aplicacion
{
    public class JusApplication: IPrincipal
    {
        public bool IsInRole(string role)
        {
            return true;
        }

        private IIdentity _identity;

        public IIdentity Identity {
            get { return _identity ?? (_identity = new JusIdentity()); }
        }
    }
}