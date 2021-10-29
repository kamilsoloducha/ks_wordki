using System;

namespace Blueprints.Domain
{

    public class BuissnessRuleFailedException : Exception
    {
        public IBuissnessRule Rule { get; }
        public BuissnessRuleFailedException(IBuissnessRule rule) : base(rule.Message)
        {
            Rule = rule;
        }
    }
}
