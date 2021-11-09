using System;
using System.Threading.Tasks;
using Blueprints.Application.Authentication;
using Blueprints.Infrastrcuture.Authentication;
using Blueprints.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application;
using Users.Domain;

namespace Users.Infrastructure
{
    public static class Module
    {
        public static IServiceCollection AddUsersInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
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
            await usersContext.Database.EnsureDeletedAsync();
            if (await usersContext.Database.EnsureCreatedAsync())
            {
                var adminUserCreator = services.GetService<AdminAccountCreator>();
                await adminUserCreator.CreateAdminUser();
            }


        }
    }
}
