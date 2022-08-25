using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class AuthorizationExtensions
    {
        public const string ChromeExtensionPolicy = "ChromeExtension";
        public const string LoginUserPolicy = "LoginUser";
        public const string AdminOnlyPolicy = "AdminOnly";
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
                {
                    options.AddPolicy(LoginUserPolicy, policy => policy.RequireRole(Users.Domain.Role.Student.Type.ToString()));
                    options.AddPolicy(AdminOnlyPolicy, policy => policy.RequireRole(Users.Domain.Role.Admin.Type.ToString()));
                    options.AddPolicy(ChromeExtensionPolicy, policy => policy.RequireRole(Users.Domain.Role.ChromeExtension.Type.ToString()));
                });
            return services;
        }
    }
}