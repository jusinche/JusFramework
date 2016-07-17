using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JusFramework.Bl.ValidacionDatos
{
    [Serializable]
    public class PersonaNombreAttribute : ValidationBaseAttribute
    {
        public PersonaNombreAttribute()
            : base(ConstantesValidaciones.NombrePersonaReg)
         {
             ErrorMessage = "El campo {0} no es correcto, verifique los datos.";
         }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectType.GetProperty(GetPropertyName(validationContext));
            if (obj!=null)
            {
                validationContext.ObjectType.GetProperty(GetPropertyName(validationContext))
                    .SetValue(validationContext.ObjectInstance, value.ToString().Trim().ToUpper(), null);
            }
                
            return base.IsValid(value, validationContext);
        }
    }
}
