using Blueprints.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Blueprints.Infrastrcuture
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddIConnectionStringProvider(this IServiceCollection services, IConfiguration configuration)
        {
            const string envTag = "ASPNETCORE_ENVIRONMENT";
            var envrionment = configuration.GetValue<string>(envTag);
            if (string.IsNullOrEmpty(envrionment))
            {
                Log.Error($"There is no {envTag} in configuration");
                throw new System.Exception($"There is no {envTag} in configuration");
            }
            return services.AddSingleton<IConnectionStringProvider, HerokuConnectionStringProvider>();
            // return envrionment.Equals("Production")
            //     ? services.AddSingleton<IConnectionStringProvider, HerokuConnectionStringProvider>()
            //     : services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
        }
    }
}