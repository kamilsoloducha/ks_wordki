using E2e.Tests;
using Users.Application.Commands;
using Users.E2e.Tests.Login;

namespace Api.Tests.Users;

public class SimpleLogin : LoginSuccessContext
{
    public override LoginUser.Command GivenRequest { get; } = new("Name", TestBase.UserPassword);
}