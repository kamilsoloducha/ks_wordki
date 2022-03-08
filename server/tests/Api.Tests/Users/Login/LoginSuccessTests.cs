using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Tests.Utils;
using FizzWare.NBuilder;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Users.Application;
using Wordki.Tests.E2E.Feature;

namespace Api.Tests.Users
{
    [TestFixture]
    public class LoginSuccessTest : UsersTestBase
    {

        private UserTest _user;

        [SetUp]
        public async Task Setup()
        {
            _user = UserBuilder.Default.With(x => x.Password = TestServerMock.MockPassword).Build();

            await ClearUsersSchema();
            using var dbContext = new TestDbContext();
            dbContext.Users.Add(_user);
            dbContext.SaveChanges();
        }

        [Test]
        public async Task Test()
        {
            var content = new LoginUser.Command
            {
                UserName = _user.Name,
                Password = _user.Password,
            };

            Request = new HttpRequestMessage(HttpMethod.Put, "users/login");
            Request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            await SendRequest();

            Response.IsSuccessStatusCode.Should().BeTrue();

            var responseContent = await Response.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseBaseTest<LoginUser.Response>>(responseContent);

            response.Should().NotBeNull();
            response.IsCorrect.Should().Be(true);
            response.Error.Should().BeNull();
            response.Response.Should().NotBe(Guid.Empty);

            using var dbContext = new TestDbContext();
            var users = dbContext.Users.ToList();
            users.Should().HaveCount(1);
            var user = users[0];
            // user.Id.Should().Be(response.Response);
            // user.Name.Should().Be(content.Name);
            // user.Email.Should().Be(content.Email);
            user.Status.Should().Be(1);
            user.ConfirmationDate.Should().Be(_user.ConfirmationDate);
            user.CreationDate.Should().Be(_user.ConfirmationDate);
            user.LoginDate.Should().Be(TestServerMock.MockDate);
            user.Password.Should().Be(TestServerMock.MockPassword);
        }
    }
}