using Application.Authentication;
using Infrastructure;
using Infrastructure.Authentication;
using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application;
using Users.Application.Services;
using Users.Domain.User;
using Users.Infrastructure.DataAccess;
using Users.Infrastructure.Repositories;
using Users.Infrastructure.Services;
using Users.Infrastructure.Services.AdminUserCreator;

namespace Users.Infrastructure;

public static class Module
{
    public static IServiceCollection AddUsersInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddUsersApplicationModule();
        services.JwtConfig(configuration);
        services.BindConfiguration<DatabaseConfiguration>(configuration);
        services.Configure<AdminAccountConfiguration>(options => configuration.GetSection(nameof(AdminAccountConfiguration)).Bind(options));

        services.AddDbContext<UsersContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordManager, PasswordManager>();
        // services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<AdminAccountCreator>();
        return services;
    }

    public static WebApplication CreateUserScheme(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersContext>();
        dbContext.Creator.EnsureCreated();
        return app;
    }
}