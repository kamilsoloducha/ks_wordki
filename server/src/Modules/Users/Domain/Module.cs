using Microsoft.Extensions.DependencyInjection;
using Users.Domain.User.Services;

namespace Users.Domain
{
    public static class Module
    {
        public static IServiceCollection AddUsersDomainModule(this IServiceCollection services)
        {
            services.AddScoped<IDataChecker, DataChecker>();
            return services;
        }
    }
}