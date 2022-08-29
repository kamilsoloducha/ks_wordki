using Api.Tests.Model.Users;
using Users.Application.Commands;

namespace Api.Tests.Users;

public abstract class RegisterUserContext
{
    public abstract RegisterUser.Command GivenRequest { get; }
    public abstract User ExpectedUser { get; }
}