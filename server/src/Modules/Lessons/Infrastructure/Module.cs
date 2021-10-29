using System;
using System.Threading.Tasks;
using Blueprints.Infrastructure.DataAccess;
using Lessons.Domain;
using Lessons.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lessons.Infrastructure
{
    public static class LessonsInfrastructureModule
    {
        public static IServiceCollection AddLessonsInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfiguration>(options => configuration.GetSection(nameof(DatabaseConfiguration)).Bind(options));

            services.AddDbContext<LessonsContext>();
            services.AddScoped<IPerformanceRepository, PerformanceRepository>();
            services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
            return services;
        }

        public static async Task CreateLessonsDb(this IServiceProvider services)
        {
            var lessonsContext = services.GetService<LessonsContext>();
            var creator = lessonsContext.Creator;
            await creator.CreateTablesAsync();
        }
    }
}