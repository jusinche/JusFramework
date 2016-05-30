using Csla.Core;
using Csla.Rules;

namespace JusFramework.Bl.Rules.AuthorizationRules
{
    public class AuthorizationBorrar : Csla.Rules.AuthorizationRule
    {
        // TODO: Add additional parameters to your rule to the constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationBorrar"/> class.
        /// </summary>
        /// <param name="action">Action this rule will enforce.</param>
        /// <param name="element">Method or property.</param>
        public AuthorizationBorrar(AuthorizationActions action, IMemberInfo element)
            : base(action, element)
        {
            // TODO: Add additional constructor code here 

        }


        // TODO: Add additional parameters to your rule to the constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationBorrar"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public AuthorizationBorrar(AuthorizationActions action)
            : base(action)
        {
            // TODO: Add additional constructor code here 

        }

        // TODO: Uncomment this property if rule result is not static. 
        /// <summary>
        ///   Notify RuelEngine that the result of this AuthzRule can not be cached. 
        ///   Default is true so AuthzRules will only run once.
        /// </summary>
        //public override bool CacheResult
        //{
        //   get { return false; }
        //}

        protected override void Execute(AuthorizationContext context)
        {
            // TODO: Add actual rule code here. 
            //if (!access_condition)
            //{
            //  context.HasPermission = false;
            //}
        }
    }
}