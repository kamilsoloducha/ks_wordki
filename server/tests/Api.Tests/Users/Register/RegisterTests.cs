using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Users.Application;
using Users.Domain;

namespace Api.Tests.Users
{
    [TestFixture]
    public class RegisterTests : UsersTestBase
    {
        [Test]
        public async Task Test()
        {
            var content = new RegisterUser.Command
            {
                Name = "username",
                Password = "password",
                Email = "user@mail.com"
            };

            Host.UserRepositoryMock
                .Setup(x => x.Any(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            Request = new HttpRequestMessage(HttpMethod.Post, "users/register");
            Request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            await SendRequest();

            var responseContent = await Response.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseBase<Guid>>(responseContent);

            response.Should().NotBeNull();
            response.IsCorrect.Should().Be(true);
            response.Response.Should().NotBe(Guid.Empty);
            response.Error.Should().BeEmpty();
        }
    }
}