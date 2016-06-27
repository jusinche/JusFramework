using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JusFramework.Bl.ValidacionDatos
{
    public class PersonaNombreAttribute : RegularExpressionAttribute
    {
        public PersonaNombreAttribute()
            : base(@"[A-Z]+[A-Z\s]*[A-Z]+")
         {
             this.ErrorMessage = "INGRESE UN NOMBRE VALIDO";
            
         }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value != null && value.ToString() != string.Empty)
                {
                    validationContext.ObjectType.GetProperty(validationContext.DisplayName)
                       .SetValue(validationContext.ObjectInstance, value.ToString().ToUpper(), null);
                }
            }
            catch (Exception)
            {
            }
            
            return null;
        }
    }
}
