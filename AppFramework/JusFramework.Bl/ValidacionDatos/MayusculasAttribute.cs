using System;
using System.ComponentModel.DataAnnotations;

namespace JusFramework.Bl.ValidacionDatos
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Mayusculas2Attribute : ValidationAttribute
    {
        //public override IModelBinder GetBinder()
        //{
        //    return new UppercaseModelBinder();
        //}
        public override string ToString()
        {
            return base.ToString().ToUpper();
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value.ToString().ToUpper());
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                validationContext.ObjectType.GetProperty(validationContext.DisplayName)
                .SetValue(validationContext.ObjectInstance, value.ToString().ToUpper(), null);
            }
            catch (Exception)
            {
            }
            return base.IsValid(value, validationContext);
        }
    }

    //public class UppercaseModelBinder : DefaultModelBinder
    //{
    //    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        var value = base.BindModel(controllerContext, bindingContext);
    //        var strValue = value as string;
    //        if (strValue == null)
    //            return value;
    //        return strValue.ToUpper();
    //    }
    //}
}
