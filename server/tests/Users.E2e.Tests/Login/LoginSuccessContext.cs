using System;
using E2e.Model.Tests.Model.Users;
using E2e.Tests;
using Users.Application.Commands;

namespace Users.E2e.Tests.Login
{
    public abstract class LoginSuccessContext
    {
        public virtual User GivenUser { get; } = new()
        {
            Id = Guid.NewGuid(),
            Email = "Email",
            Name = "Name",
            Password = TestBase.UserHashedPassword,
            Status = 1,
            Surname = "Surname",
            FirstName = "FirstName",
            ConfirmationDate = TestServerMock.MockDate,
            CreationDate = TestServerMock.MockDate,
            LoginDate = new DateTime(),
            Roles = new[] { new Role { Id = 1, Type = 1 } }
        };
    
        public abstract LoginUser.Command GivenRequest { get; }
    }
}