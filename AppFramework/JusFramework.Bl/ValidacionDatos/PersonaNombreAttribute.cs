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
             ErrorMessage = "El campo {0} no es correcto, verifique los datos.";
         }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
                validationContext.ObjectType.GetProperty(GetPropertyName(validationContext))
                    .SetValue(validationContext.ObjectInstance, value.ToString().Trim().ToUpper(), null);
            return base.IsValid(value, validationContext);
        }

        private string GetPropertyName(ValidationContext validationContext)
        {
            var property = validationContext.DisplayName;
            foreach (var propertyInfo in validationContext.ObjectType.GetProperties())
            {
                foreach (var customAttributeData in propertyInfo.CustomAttributes)
                {
                    if (customAttributeData.AttributeType == typeof(DisplayAttribute))
                    {
                        if (customAttributeData.NamedArguments != null && customAttributeData.NamedArguments.First().TypedValue.Value.ToString() == validationContext.DisplayName)
                        {
                            return propertyInfo.Name;
                        }
                    }
                }
            }
            return property;
        }
    }
}
