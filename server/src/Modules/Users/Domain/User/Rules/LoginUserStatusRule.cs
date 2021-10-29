using System.Threading;
using System.Threading.Tasks;
using Blueprints.Domain;

namespace Users.Domain
{
    internal class LoginUserStatusRule : IBuissnessRule
    {
        private readonly RegistrationStatus _status;
        public string Message { get; } = "Only confirmed user can log in";

        public LoginUserStatusRule(RegistrationStatus status)
        {
            _status = status;
        }

        public Task<bool> IsCorrect(CancellationToken cancellationToken)
            => Task.FromResult(_status == RegistrationStatus.Registered);
    }
}

