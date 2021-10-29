using System.Threading;
using System.Threading.Tasks;
using Blueprints.Domain;

namespace Cards.Domain
{
    internal class RegisterAnswerIsUsedRule : IBuissnessRule
    {
        private readonly bool _isUsed;
        public string Message { get; } = "CardSide which is not used, cannot be used in teaching";

        public RegisterAnswerIsUsedRule(bool isUsed)
        {
            _isUsed = isUsed;
        }
        public Task<bool> IsCorrect(CancellationToken cancellationToken)
            => Task.FromResult(_isUsed);
    }
}