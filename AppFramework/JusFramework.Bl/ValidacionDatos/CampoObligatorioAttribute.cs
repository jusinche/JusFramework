using System;
using System.ComponentModel.DataAnnotations;

namespace JusFramework.Bl.ValidacionDatos
{
    [Serializable]
    public class CampoObligatorioAttribute : RequiredAttribute
    {
        public CampoObligatorioAttribute() 

        {
            ErrorMessage="Ingrese un valor en {0}, este campo es obligatorio.";
        }
    }
}
