using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using E2e.Model.Tests.Model.Users;
using E2e.Tests;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Users.Application.Commands;

namespace Users.E2e.Tests.Login;

[TestFixture(typeof(SimpleLogin))]
public class LoginSuccessTests<TContext> : UsersTestBase where TContext : LoginSuccessContext, new()
{
    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        await ClearUsersSchema();
        await using var dbContext = new UsersContext();
        await dbContext.Users.AddAsync(_context.GivenUser);
        await dbContext.SaveChangesAsync();

        AuthenticationServiceMock.Setup(x =>
            x.Authenticate(
                _context.GivenUser.Id,
                It.IsAny<IEnumerable<string>>()
                )
            ).Returns(TestServerMock.MockToken);
    }

    [Test]
    public async Task Test()
    {
        var request = JsonConvert.SerializeObject(_context.GivenRequest);

        Request = new HttpRequestMessage(HttpMethod.Put, "users/login")
        {
            Content = new StringContent(request, Encoding.UTF8, "application/json")
        };

        await SendRequest();

        var responseContent = await Response.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<LoginUser.Response>(responseContent);
        
        response.Should().NotBeNull(responseContent);
        response.ResponseCode.Should().Be(LoginUser.ResponseCode.Successful, responseContent);
        response.Token.Should().Be(TestServerMock.MockToken, responseContent);
        response.Id.Should().Be(_context.GivenUser.Id, responseContent);

        await using var dbContext = new UsersContext();
        var users = dbContext.Users.ToList();
        users.Should().HaveCount(1);
        var user = users[0];
        user.LoginDate.Should().Be(TestServerMock.MockDate);
    }
}