using Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services.HashIds;

public static class HashIdsExtensions
{
    public static IServiceCollection AddHashIds(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
    {
        services.BindConfiguration<HashIdsConfiguration>(configuration);

        if (hostingEnvironment.IsProduction())
            services.AddSingleton<IHashIdsService, HashIdsService>();
        else
            services.AddSingleton<IHashIdsService, TestHashIdsService>();
        return services;
    }
}