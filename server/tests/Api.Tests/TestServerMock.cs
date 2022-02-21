using Api;
using Blueprints.Application.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Users.Domain;
using Wordki.Tests.Utils.ServerMock;

namespace Wordki.Tests.E2E.Feature
{
    public class TestServerMock : ServerMock<Startup>
    {
        public Mock<IAuthenticationService> AuthenticationServiceMock { get; private set; }
        public Mock<IUserRepository> UserRepositoryMock { get; private set; }

        protected override void ConfigureTestContainer(IServiceCollection services)
        {
            services.AddScoped<IUserRepository>(s => UserRepositoryMock.Object);
        }

        protected override void CreateMocks()
        {
            AuthenticationServiceMock = new Mock<IAuthenticationService>();
            UserRepositoryMock = new Mock<IUserRepository>();
        }
    }
}
