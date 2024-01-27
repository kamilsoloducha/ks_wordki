using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Authentication;
using Application.Services;
using Infrastructure.Services.ConnectionStringProvider;
using Infrastructure.Services.HashIds;
using MassTransit;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Users.Application.Services;

namespace E2e.Tests;

public abstract class TestBase
{
    public const string UserPassword = "Password";
    public const string UserHashedPassword = "HashedPassword";
    
    protected E2EWebApplicationFactory AppFactory { get; }
        
    protected HttpRequestMessage Request { get; set; }
    protected HttpResponseMessage Response { get; private set; }
    protected virtual Action<IServiceCollection> TestServiceConfig { get; } = _ => { };

    protected  Mock<IPasswordManager> PasswordManagerMock { get; }
    protected Mock<IPublishEndpoint> PublishEndpointMock { get; }
    protected Mock<IAuthenticationService> AuthenticationServiceMock { get; }
    
    protected TestBase()
    {
        AppFactory = new E2EWebApplicationFactory(ServiceConfig);
        
        PasswordManagerMock = new Mock<IPasswordManager>();
        PasswordManagerMock.Setup(x => x.CreateHashedPassword(UserPassword)).Returns(UserHashedPassword);

        PublishEndpointMock = new Mock<IPublishEndpoint>();

        AuthenticationServiceMock = new Mock<IAuthenticationService>();
    }

    private void ServiceConfig(IServiceCollection services)
    {
        services.AddSingleton(_ => PasswordManagerMock.Object);
        services.AddSingleton(_ => PublishEndpointMock.Object);
        services.AddSingleton(_ => AuthenticationServiceMock.Object);
        
        services.AddSingleton<IHashIdsService, TestHashIdsService>();
        services.AddSingleton<IPolicyEvaluator, DisableAuthenticationPolicyEvaluator>();

        TestServiceConfig.Invoke(services);
    }
        
    protected async Task SendRequest()
    {
        Response = await AppFactory.HttpClient.Value.SendAsync(Request);
    }
    
    protected DbContextOptions<TContext> GetDbContextOptions<TContext>() where TContext : DbContext
    {
        var connectionStringProvider = AppFactory.Services.GetRequiredService<IConnectionStringProvider>();
        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        optionsBuilder.UseNpgsql(connectionStringProvider.ConnectionString);
        return optionsBuilder.Options;
    }
}