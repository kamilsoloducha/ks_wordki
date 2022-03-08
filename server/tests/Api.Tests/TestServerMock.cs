using System;
using System.Collections.Generic;
using Api;
using Blueprints.Application.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Users.Application;
using Wordki.Tests.Utils.ServerMock;

namespace Wordki.Tests.E2E.Feature
{
    public class TestServerMock : ServerMock<Startup>
    {
        public const string MockPassword = "MockPassword";
        public const string MockToken = "MockToken";
        public static readonly DateTime MockDate = new DateTime(2022, 1, 1, 10, 0, 0);

        public Mock<IAuthenticationService> AuthenticationServiceMock { get; private set; }
        public Mock<IPasswordManager> PasswordManagerMock { get; private set; }

        protected override void ConfigureTestContainer(IServiceCollection services)
        {
            services.AddSingleton<IPasswordManager>(x => PasswordManagerMock.Object);
            services.AddSingleton<IAuthenticationService>(x => AuthenticationServiceMock.Object);
        }

        protected override void CreateMocks()
        {
            AuthenticationServiceMock = new Mock<IAuthenticationService>();
            AuthenticationServiceMock.Setup(x => x.Authenticate(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>())).Returns(MockToken);

            PasswordManagerMock = new Mock<IPasswordManager>();
            PasswordManagerMock.Setup(x => x.CreateHashedPassword(It.IsAny<string>())).Returns(MockPassword);
        }
    }
}
