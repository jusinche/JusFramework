using Csla.Core;
using Csla.Rules;

namespace JusFramework.Bl.Rules
{
    /// <summary>
    /// Permite disparar la regla en base a un delegado para validar una propiedad o mas cuando hay dependencia.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidateRule<T> : BusinessRule
    {
        // Declaration
        public delegate bool DelegateRule(T obj,RuleContextArg args);

        private readonly DelegateRule _delegateRule;
        /// <summary>
        /// Valida una regla
        /// </summary>
        /// <param name="delegateRule"></param>
        /// <param name="primaryProperty"></param>
        /// <param name="secundaryProperty"></param>
        public ValidateRule(DelegateRule delegateRule, IPropertyInfo primaryProperty, IPropertyInfo secundaryProperty=null)
            : base(primaryProperty)
        {
            _delegateRule=delegateRule;
            if (secundaryProperty!=null)
            {
                InputProperties.Add(primaryProperty);
                InputProperties.Add(secundaryProperty);
            }
        }

        protected override void Execute(RuleContext context)
        {
            var args=new RuleContextArg();
            if (!_delegateRule((T) context.Target, args))
            {
                context.AddErrorResult(args.Description);
            }
        }
    }
}