using Users.Application.Commands;

namespace Api.Tests.Users;

public class SimpleLogin : LoginSuccessContext
{
    public override LoginUser.Command GivenRequest { get; } = new()
    {
        UserName = "Name",
        Password = "Password"
    };
}