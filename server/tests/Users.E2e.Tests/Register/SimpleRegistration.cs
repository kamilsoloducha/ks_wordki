using System;
using System.Linq;
using System.Threading.Tasks;
using E2e.Model.Tests.Model.Users;
using E2e.Tests;
using FluentAssertions;
using Newtonsoft.Json;
using Users.Application.Commands;

namespace Users.E2e.Tests.Register;

public class SimpleRegistration : RegisterUserContext
{
    public override RegisterUser.Command GivenRequest { get; } 
        = new ("username", TestBase.UserPassword, "user@email.com", null, null);

    public override User ExpectedUser { get; } = new()
    {
        Name = "username",
        Email = "user@email.com",
        Password = TestBase.UserHashedPassword,
        ConfirmationDate = TestServerMock.MockDate,
        CreationDate = TestServerMock.MockDate,
        Surname = string.Empty,
        FirstName = string.Empty,
        LoginDate = DateTime.MinValue,
        Status = 1
    };
}