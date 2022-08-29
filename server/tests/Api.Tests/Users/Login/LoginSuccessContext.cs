using System;
using Api.Tests.Model.Users;
using Users.Application.Commands;
using Wordki.Tests.E2E.Feature;

namespace Api.Tests.Users;

public abstract class LoginSuccessContext
{
    public virtual User GivenUser { get; } = new()
    {
        Id = Guid.NewGuid(),
        Email = "Email",
        Name = "Name",
        Password = TestServerMock.MockPassword,
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