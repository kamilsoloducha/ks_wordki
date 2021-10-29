using Microsoft.Extensions.DependencyInjection;

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