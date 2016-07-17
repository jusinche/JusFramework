using System;
using System.Collections;
using Csla.Core;
using Csla.Rules;

namespace JusFramework.Bl.Rules
{
    [Serializable]
    public class ListaConElementosRule: BusinessRule
    {
        // Declaration
        //public delegate RuleContextArg DelegateRule(T obj, IPropertyInfo primaryProperty,RuleContextArg args);

        //private DelegateRule _delegateRule;
        public ListaConElementosRule(IPropertyInfo primaryProperty)
            : base(primaryProperty)
        {
        }

        protected override void Execute(RuleContext context)
        {
            var obj=context.Target.GetType().GetProperty(PrimaryProperty.Name).GetValue(context.Target);
            var msj = String.Format("Agregue al menos un elemento en {0}.", PrimaryProperty.FriendlyName);
            var list = obj as IList;
            if (list==null)
            {
                context.AddErrorResult(msj);
                return;
            }
            if (!(list.Count>0))
            {
                context.AddErrorResult(msj);
            }
        }
    }
    
}