using System;
using E2e.Tests;
using Users.Application.Commands;
using Users.E2e.Tests.Models.Users;

namespace Users.E2e.Tests.Register;

public class SimpleRegistration : RegisterUserContext
{
    public override RegisterUser.Command GivenRequest { get; } 
        = new ("userName", TestBase.UserPassword, "user@email.com", null, null);

    public override User ExpectedUser { get; } = new()
    {
        Name = "username",
        Email = "user@mail.com",
        Password = TestBase.UserHashedPassword,
        ConfirmationDate = TestServerMock.MockDate,
        CreationDate = TestServerMock.MockDate,
        Surname = string.Empty,
        FirstName = string.Empty,
        LoginDate = DateTime.MinValue,
        Status = 1
    };
}