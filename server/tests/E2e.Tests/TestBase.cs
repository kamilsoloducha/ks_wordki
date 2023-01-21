using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Authentication;
using Application.Services;
using Domain.Utils;
using Infrastructure.Services.HashIds;
using MassTransit;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Users.Application.Services;
using Users.Domain.User;

namespace E2e.Tests;

public abstract class TestBase
{

    public const string UserPassword = "Password";
    public const string UserHashedPassword = "HashedPassword";
    
    protected E2EWebApplicationFactory AppFactory { get; }
        
    public HttpRequestMessage Request { get; set; }
    public HttpResponseMessage Response { get; private set; }
    protected virtual Action<IServiceCollection> TestServiceConfig { get; } = _ => { };

    protected  Mock<IPasswordManager> PasswordManagerMock { get; }
    protected  Mock<IAuthenticationService> AuthenticationServiceMock { get; }
    protected Mock<IPublishEndpoint> PublishEndpointMock { get; }
    
    protected TestBase()
    {
        AppFactory = new E2EWebApplicationFactory(ServiceConfig);
        SystemClock.Override(new DateTime(2022, 1, 1, 10, 0 ,0));
        
        PasswordManagerMock = new Mock<IPasswordManager>();
        PasswordManagerMock.Setup(x => x.CreateHashedPassword(UserPassword)).Returns(UserHashedPassword);

        AuthenticationServiceMock = new Mock<IAuthenticationService>();
        AuthenticationServiceMock.Setup(x => x.Authenticate(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>())).Returns("Token");

        PublishEndpointMock = new Mock<IPublishEndpoint>();
    }

    private void ServiceConfig(IServiceCollection services)
    {
        services.AddSingleton(_ => PasswordManagerMock.Object);
        services.AddSingleton(_ => AuthenticationServiceMock.Object);
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