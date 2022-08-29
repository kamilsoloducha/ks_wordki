using System;
using Api.Tests.Model.Users;
using Users.Application.Commands;
using Wordki.Tests.E2E.Feature;

namespace Api.Tests.Users;

public class SimpleRegistration : RegisterUserContext
{
    public override RegisterUser.Command GivenRequest { get; } = new()
    {
        UserName = "username",
        Password = "password",
        Email = "user@mail.com"
    };

    public override User ExpectedUser { get; } = new()
    {
        Name = "username",
        Email = "user@mail.com",
        Password = TestServerMock.MockPassword,
        ConfirmationDate = TestServerMock.MockDate,
        CreationDate = TestServerMock.MockDate,
        Surname = string.Empty,
        FirstName = string.Empty,
        LoginDate = DateTime.MinValue,
        Status = 1
    };
}