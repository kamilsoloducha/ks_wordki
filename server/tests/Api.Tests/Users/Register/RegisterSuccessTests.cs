// using System;
// using System.Linq;
// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using FluentAssertions;
// using Newtonsoft.Json;
// using NUnit.Framework;
// using Users.Application;
// using Wordki.Tests.E2E.Feature;

// namespace Api.Tests.Users
// {
//     [TestFixture(Ignore = "not ready")]
//     public class RegisterSuccessTests : UsersTestBase
//     {
//         [SetUp]
//         public async Task Setup()
//         {
//             await ClearUsersSchema();
//         }

//         [Test]
//         public async Task Test()
//         {
//             var content = new RegisterUser.Command
//             {
//                 UserName = "username",
//                 Password = "password",
//                 Email = "user@mail.com"
//             };

//             Request = new HttpRequestMessage(HttpMethod.Post, "users/register");
//             Request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

//             await SendRequest();

//             var responseContent = await Response.Content.ReadAsStringAsync();
//             var response = JsonConvert.DeserializeObject<ResponseBaseTest<Guid>>(responseContent);

//             response.Should().NotBeNull();
//             response.IsCorrect.Should().Be(true);
//             response.Error.Should().BeNull();
//             response.Response.Should().NotBe(Guid.Empty);

//             using var dbContext = new TestDbContext();
//             var users = dbContext.Users.ToList();
//             users.Should().HaveCount(1);
//             var user = users[0];
//             user.Id.Should().Be(response.Response);
//             user.Name.Should().Be(content.UserName);
//             user.Email.Should().Be(content.Email);
//             user.Status.Should().Be(1);
//             user.ConfirmationDate.Should().Be(TestServerMock.MockDate);
//             user.CreationDate.Should().Be(TestServerMock.MockDate);
//             user.Password.Should().Be(TestServerMock.MockPassword);
//         }
//     }
// }