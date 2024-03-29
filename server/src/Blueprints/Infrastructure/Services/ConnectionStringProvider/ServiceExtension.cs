using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.ConnectionStringProvider;

public static class ServiceExtension
{
    public static IServiceCollection AddConnectionStringProvider(this IServiceCollection services, IConfiguration configuration)
    {
        // const string envTag = "ASPNETCORE_ENVIRONMENT";
        // var envrionment = configuration.GetValue<string>(envTag);
        // if (string.IsNullOrEmpty(envrionment))
        // {
        //     Log.Error($"There is no {envTag} in configuration");
        //     throw new System.Exception($"There is no {envTag} in configuration");
        // }

        return HerokuConnectionStringProvider.IsHerokuEnv(configuration)
            ? services.AddSingleton<IConnectionStringProvider, HerokuConnectionStringProvider>()
            : services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
    }
}