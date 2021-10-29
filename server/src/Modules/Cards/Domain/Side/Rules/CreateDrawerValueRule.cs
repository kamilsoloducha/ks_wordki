using System.Threading;
using System.Threading.Tasks;
using Blueprints.Domain;

namespace Cards.Domain
{
    internal class CreateDrawerValueRule : IBuissnessRule
    {
        private readonly int _value;

        public string Message { get; } = "Drawer value must be between 1 and 5";

        public CreateDrawerValueRule(int value)
        {
            _value = value;
        }

        public Task<bool> IsCorrect(CancellationToken cancellationToken)
            => Task.FromResult(_value >= 1 && _value <= 5);
    }
}