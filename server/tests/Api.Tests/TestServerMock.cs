using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Users.Application;
using Wordki.Tests.Utils.ServerMock;
using IAuthenticationService = Blueprints.Application.Authentication.IAuthenticationService;

namespace Api.Tests;

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
        services.AddSingleton<IHashIdsService>(x => new TestHashIdsService());
        services.AddSingleton<IPolicyEvaluator, DisableAuthenticationPolicyEvaluator>();

    }

    protected override void CreateMocks()
    {
        AuthenticationServiceMock = new Mock<IAuthenticationService>();
        AuthenticationServiceMock.Setup(x => x.Authenticate(It.IsAny<Guid>(), It.IsAny<IEnumerable<string>>())).Returns(MockToken);

        PasswordManagerMock = new Mock<IPasswordManager>();
        PasswordManagerMock.Setup(x => x.CreateHashedPassword(It.IsAny<string>())).Returns(MockPassword);
    }
}

public class DisableAuthenticationPolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var authenticationTicket = new AuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties(),
            JwtBearerDefaults.AuthenticationScheme);
        return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context,
        object resource)
    {
        return Task.FromResult(PolicyAuthorizationResult.Success());
    }
}