using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Role;

namespace Api.Configuration;

public static class AuthorizationExtensions
{
    public const string ChromeExtensionPolicy = "ChromeExtension";
    public const string LoginUserPolicy = "LoginUser";
    public const string AdminOnlyPolicy = "AdminOnly";
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(LoginUserPolicy, policy => policy.RequireRole(Role.Student.Type.ToString()));
            options.AddPolicy(AdminOnlyPolicy, policy => policy.RequireRole(Role.Admin.Type.ToString()));
            options.AddPolicy(ChromeExtensionPolicy, policy => policy.RequireRole(Role.ChromeExtension.Type.ToString()));
        });
        return services;
    }
}