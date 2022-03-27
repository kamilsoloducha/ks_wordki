using Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public static class HashIdsExtentions
    {
        public static IServiceCollection AddHashIds(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            services.Configure<HashIdsConfiguration>(options => configuration.GetSection(nameof(HashIdsConfiguration)).Bind(options));

            if (hostingEnvironment.EnvironmentName == "Development")
                services.AddSingleton<IHashIdsService, TestHashIdsService>();
            else
                services.AddSingleton<IHashIdsService, HashIdsService>();
            return services;
        }
    }
}