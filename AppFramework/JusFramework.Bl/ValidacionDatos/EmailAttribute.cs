using System;
using System.ComponentModel.DataAnnotations;

namespace JusFramework.Bl.ValidacionDatos
{
    [Serializable]
    public class EmailAttribute : ValidationBaseAttribute
    {
         public EmailAttribute()
            : base(ConstantesValidaciones.EmailReg)

         {
             ErrorMessage = "Ingrese una dirección de correo electrónica valida.";
         }
    }
}
