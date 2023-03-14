using E2e.Tests;
using Users.Application.Commands;

namespace Users.E2e.Tests.Login
{
    public class SimpleLogin : LoginSuccessContext
    {
        public override LoginUser.Command GivenRequest { get; } = new("Name", TestBase.UserPassword);
    }
}