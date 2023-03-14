using System.Threading;
using System.Threading.Tasks;

namespace Domain.Rules
{
    public abstract class StringValueHaveToBeDefined : IBuissnessRule
    {
        private readonly string _value;
        public string Message { get; }

        public StringValueHaveToBeDefined(string value, string propertyName)
        {
            _value = value;
            Message = $"{propertyName} have to be defined";
        }

        public Task<bool> IsCorrect(CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(_value));
        }
    }
}