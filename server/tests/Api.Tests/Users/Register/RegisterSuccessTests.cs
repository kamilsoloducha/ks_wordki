using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Tests.Model.Users;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Users.Application.Commands;
using Wordki.Tests.E2E.Feature;

namespace Api.Tests.Users;

[TestFixture(typeof(SimpleRegistration))]
public class RegisterSuccessTests<TContext> : UsersTestBase where TContext : RegisterUserContext, new()
{
    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        await ClearUsersSchema();
    }

    [Test]
    public async Task Test()
    {
        var request = JsonConvert.SerializeObject(_context.GivenRequest);

        Request = new HttpRequestMessage(HttpMethod.Post, "users/register")
        {
            Content = new StringContent(request, Encoding.UTF8, "application/json")
        };

        await SendRequest();

        var responseContent = await Response.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<RegisterUser.Response>(responseContent);

        response.Should().NotBeNull();
        response.ResponseCode.Should().Be(RegisterUser.ResponseCode.Successful, responseContent);
        response.UserId.Should().NotBeNull(responseContent);

        await using var dbContext = new UsersContext();
        var users = dbContext.Users.ToList();
        users.Should().HaveCount(1);
        var user = users[0];
        user.Id.Should().Be(response.UserId.Value);
        user.Name.Should().Be(_context.ExpectedUser.Name);
        user.Email.Should().Be(_context.ExpectedUser.Email);
        user.Status.Should().Be(_context.ExpectedUser.Status);
        user.ConfirmationDate.Should().Be(_context.ExpectedUser.ConfirmationDate);
        user.CreationDate.Should().Be(_context.ExpectedUser.ConfirmationDate);
        user.Password.Should().Be(_context.ExpectedUser.Password);
    }
}

public abstract class RegisterUserContext
{
    public abstract RegisterUser.Command GivenRequest { get; }
    public abstract User ExpectedUser { get; }
}

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