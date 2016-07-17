using Csla.Core;
using Csla.Rules;

namespace JusFramework.Bl.Rules
{
    public class ValidateRule<T> : BusinessRule
    {
        // Declaration
        public delegate bool DelegateRule(T obj,RuleContextArg args);

        private readonly DelegateRule _delegateRule;
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