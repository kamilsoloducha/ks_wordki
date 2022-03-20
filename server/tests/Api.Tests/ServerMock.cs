using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System;
using Moq;
using Users.Application;
using Blueprints.Application.Authentication;
using System.Collections.Generic;

namespace Wordki.Tests.Utils.ServerMock
{
    public abstract class ServerMock<TStartup> where TStartup : class
    {
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }

        public ServerMock()
        {
            CreateMocks();
            CreateServer();
        }

        private void CreateServer()
        {
            Server = new TestServer(new WebHostBuilder()
                .ConfigureTestServices(ConfigureTestContainer)
                .ConfigureServices(ConfigureServices)
                .UseEnvironment("Test")
                .UseStartup<TStartup>());
            Client = Server.CreateClient();
        }

        protected abstract void CreateMocks();
        protected abstract void ConfigureTestContainer(IServiceCollection services);
        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new AllowAnonymousFilter());
            });
        }
    }

    public class E2eWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly Action<IServiceCollection> _serviceConfig;
        public Lazy<HttpClient> HttpClient { get; }

        public E2eWebApplicationFactory(Action<IServiceCollection> serviceConfig)
        {
            HttpClient = new Lazy<HttpClient>(CreateClient());
            _serviceConfig = serviceConfig;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.ConfigureTestServices(services =>
            {
                _serviceConfig(services);
                services.AddMvc(options =>
                    {
                        options.Filters.Add(new AllowAnonymousFilter());
                    });
            });
        }
    }

    public abstract class TestBase
    {
        private E2eWebApplicationFactory AppFactory { get; }

        protected HttpClient HttpClient => AppFactory.HttpClient.Value;
        protected virtual Action<IServiceCollection> TestServiceConfig { get; }

        protected Mock<IPasswordManager> PasswordManagerMock { get; set; }
        protected Mock<IAuthenticationService> AuthenticationServiceMock { get; }

        protected TestBase()
        {
            AppFactory = new E2eWebApplicationFactory(ServiceConfig);

            PasswordManagerMock = new Mock<IPasswordManager>();
            PasswordManagerMock.Setup(x => x.CreateHashedPassword(It.IsAny<string>())).Returns("HashedPassword");

            AuthenticationServiceMock = new Mock<IAuthenticationService>();
            AuthenticationServiceMock.Setup(x => x.Authenticate(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>())).Returns("Token");
        }

        protected void ServiceConfig(IServiceCollection services)
        {
            services.AddSingleton(x => PasswordManagerMock.Object);
            services.AddSingleton(x => AuthenticationServiceMock.Object);

            if (TestServiceConfig is not null) TestServiceConfig(services);
        }
    }
}
