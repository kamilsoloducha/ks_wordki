using System;
using System.Threading.Tasks;
using Application.Authentication;
using Infrastructure.Authentication;
using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application;
using Users.Application.Services;
using Users.Domain;
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
        services.Configure<DatabaseConfiguration>(options => configuration.GetSection(nameof(DatabaseConfiguration)).Bind(options));
        services.Configure<AdminAccountConfiguration>(options => configuration.GetSection(nameof(AdminAccountConfiguration)).Bind(options));

        services.AddDbContext<UsersContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordManager, PasswordManager>();
        // services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<AdminAccountCreator>();
        return services;
    }

    public static async Task CreateUsersDb(this IServiceProvider services)
    {

        var usersContext = services.GetService<UsersContext>();
        var creator = usersContext.Creator;
        await creator.CreateTablesAsync();

        var adminUserCreator = services.GetService<AdminAccountCreator>();
        await adminUserCreator.CreateAdminUser();
    }
}