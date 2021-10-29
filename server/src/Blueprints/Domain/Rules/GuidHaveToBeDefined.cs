
using System;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Domain;

namespace Cards.Domain
{
    public abstract class GuidHaveToBeDefined : IBuissnessRule
    {
        private readonly Guid _idValue;
        public string Message { get; }

        public GuidHaveToBeDefined(Guid idValue, string propertyName)
        {
            _idValue = idValue;
            Message = $"{propertyName} have to be defined";
        }

        public Task<bool> IsCorrect(CancellationToken cancellationToken)
        {
            return Task.FromResult(_idValue != Guid.Empty);
        }
    }
}