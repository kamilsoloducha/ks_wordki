using Blueprints.Domain;

namespace Cards.Domain.Exceptions
{
    internal class EmptyGuidException : DomainException
    {
        public EmptyGuidException() : base($"Guid cannot be empty") { }
    }
}