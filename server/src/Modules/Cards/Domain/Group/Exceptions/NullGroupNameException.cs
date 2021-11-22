using Blueprints.Domain;

namespace Cards.Domain.Exceptions
{
    public class NullGroupNameException : DomainException
    {
        public NullGroupNameException() : base("Group name cannot be empty") { }
    }
}