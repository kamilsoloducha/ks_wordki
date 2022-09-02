using Users.Application.Commands;
using Users.E2e.Tests.Models.Users;

namespace Users.E2e.Tests.Register;

public abstract class RegisterUserContext
{
    public abstract RegisterUser.Command GivenRequest { get; }
    public abstract User ExpectedUser { get; }
}