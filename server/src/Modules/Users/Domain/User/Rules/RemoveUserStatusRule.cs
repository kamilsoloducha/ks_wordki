using System.Threading;
using System.Threading.Tasks;
using Blueprints.Domain;

namespace Users.Domain
{
    internal class RemoveUserStatusRule : IBuissnessRule
    {
        private readonly RegistrationStatus _status;
        public string Message { get; } = "Only user who is registered can be removed";

        public RemoveUserStatusRule(RegistrationStatus status)
        {
            _status = status;
        }

        public Task<bool> IsCorrect(CancellationToken cancellationToken)
            => Task.FromResult(_status == RegistrationStatus.Registered ||
                _status == RegistrationStatus.WaitingForConfirmation);
    }
}

