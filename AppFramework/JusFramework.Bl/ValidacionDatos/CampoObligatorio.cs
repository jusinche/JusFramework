using System.ComponentModel.DataAnnotations;

namespace JusFramework.Bl.ValidacionDatos
{
    public class CampoObligatorioAttribute : RequiredAttribute
    {
        public CampoObligatorioAttribute() 

        {
            ErrorMessage="El campo {0} es obligatorio.";
        }
    }
}
