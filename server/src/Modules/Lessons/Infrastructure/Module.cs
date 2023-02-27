using System;
using System.Threading.Tasks;
using Infrastructure.Services.ConnectionStringProvider;
using Lessons.Application;
using Lessons.Domain.Performance;
using Lessons.Infrastructure.DataAccess;
using Lessons.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lessons.Infrastructure;

public static class LessonsInfrastructureModule
{
    public static IServiceCollection AddLessonsInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLessonsApplicationModule();
        services.Configure<DatabaseConfiguration>(options => configuration.GetSection(nameof(DatabaseConfiguration)).Bind(options));

        services.AddDbContext<LessonsContext>();
        services.AddScoped<IPerformanceRepository, PerformanceRepository>();
        // services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
        return services;
    }

    public static async Task CreateLessonsDb(this IServiceProvider services)
    {
        var lessonsContext = services.GetService<LessonsContext>();
        var creator = lessonsContext.Creator;
        await creator.CreateTablesAsync();
    }
}