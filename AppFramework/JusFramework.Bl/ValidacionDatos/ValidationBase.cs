using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JusFramework.Bl.ValidacionDatos
{
    [Serializable]
    public abstract class ValidationBaseAttribute : RegularExpressionAttribute
    {
        protected ValidationBaseAttribute(string pattern)
            : base(pattern)
        {
        }
        protected string GetPropertyName(ValidationContext validationContext)
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
