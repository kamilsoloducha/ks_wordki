using E2e.Model.Tests.Model.Users;
using Users.Application.Commands;

namespace Users.E2e.Tests.Register
{
    public abstract class RegisterUserContext
    {
        public abstract RegisterUser.Command GivenRequest { get; }
        public abstract User ExpectedUser { get; }
    }
}