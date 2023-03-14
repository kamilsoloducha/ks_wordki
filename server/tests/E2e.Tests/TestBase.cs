using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Services;
using Domain.Utils;
using Infrastructure.Services.HashIds;
using MassTransit;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Users.Application.Services;

namespace E2e.Tests
{
    public abstract class TestBase
    {
        public const string UserPassword = "Password";
        public const string UserHashedPassword = "HashedPassword";
    
        protected E2EWebApplicationFactory AppFactory { get; }
        
        public HttpRequestMessage Request { get; set; }
        public HttpResponseMessage Response { get; private set; }
        protected virtual Action<IServiceCollection> TestServiceConfig { get; } = _ => { };

        protected  Mock<IPasswordManager> PasswordManagerMock { get; }
        protected Mock<IPublishEndpoint> PublishEndpointMock { get; }
    
        protected TestBase()
        {
            AppFactory = new E2EWebApplicationFactory(ServiceConfig);
            SystemClock.Override(new DateTime(2022, 2, 20, 13, 58 ,49));
        
            PasswordManagerMock = new Mock<IPasswordManager>();
            PasswordManagerMock.Setup(x => x.CreateHashedPassword(UserPassword)).Returns(UserHashedPassword);

            PublishEndpointMock = new Mock<IPublishEndpoint>();
        }

        private void ServiceConfig(IServiceCollection services)
        {
            services.AddSingleton(_ => PasswordManagerMock.Object);
            services.AddSingleton(_ => PublishEndpointMock.Object);
        
            services.AddSingleton<IHashIdsService, TestHashIdsService>();
            services.AddSingleton<IPolicyEvaluator, DisableAuthenticationPolicyEvaluator>();

            TestServiceConfig.Invoke(services);
        }
        
        protected async Task SendRequest()
        {
            Response = await AppFactory.HttpClient.Value.SendAsync(Request);
        }
    }
}