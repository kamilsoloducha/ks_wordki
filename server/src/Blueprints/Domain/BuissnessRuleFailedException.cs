using System;
using Domain.Rules;

namespace Domain
{
    public class BuissnessRuleFailedException : Exception
    {
        public IBuissnessRule Rule { get; }
        public BuissnessRuleFailedException(IBuissnessRule rule) : base(rule.Message)
        {
            Rule = rule;
        }
    }

    public class BuissnessArgumentException : Exception
    {
        public string ArgumentName { get; }
        public object Value { get; }

        public BuissnessArgumentException(string argumentName, object value)
        {
            ArgumentName = argumentName;
            Value = value;
        }
    }


    public class BuissnessObjectNotFoundException : Exception
    {

        public string Name { get; }
        public object Id { get; }

        public BuissnessObjectNotFoundException(string name, object id)
        {
            Name = name;
            Id = id;
        }
    }
}