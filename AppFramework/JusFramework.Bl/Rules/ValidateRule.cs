using Csla.Core;
using Csla.Rules;

namespace JusFramework.Bl.Rules
{
    public class ValidateRule<T> : BusinessRule
    {
        // Declaration
        public delegate RuleContextArg DelegateRule(T obj);

        private DelegateRule _delegateRule;
        public ValidateRule(DelegateRule delegateRule, IPropertyInfo primaryProperty)
            : base(primaryProperty)
        {
            _delegateRule=delegateRule;
        }

        protected override void Execute(RuleContext context)
        {
            var regla = _delegateRule((T) context.Target);
            if (!regla.IsValid)
            {
                context.AddErrorResult(regla.Description);
            }
        }
    }

    public class RuleContextArg
    {
        public string Description { get; set; }
        public bool IsValid { get; set; }
    }
}