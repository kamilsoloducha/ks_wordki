using System.Threading;
using System.Threading.Tasks;
using Blueprints.Domain;

namespace Users.Domain
{
    public class ConfirmUserStatusRule : IBuissnessRule
    {
        private readonly RegistrationStatus _status;
        public string Message { get; } = "Only user who is waiting for confirmation can be confirmed";

        public ConfirmUserStatusRule(RegistrationStatus status)
        {
            _status = status;
        }

        public Task<bool> IsCorrect(CancellationToken cancellationToken)
            => Task.FromResult(_status == RegistrationStatus.WaitingForConfirmation);
    }
}

