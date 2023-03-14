using Infrastructure;
using Infrastructure.Services.ConnectionStringProvider;
using Lessons.Application;
using Lessons.Domain.Performance;
using Lessons.Infrastructure.DataAccess;
using Lessons.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lessons.Infrastructure
{
    public static class LessonsInfrastructureModule
    {
        public static IServiceCollection AddLessonsInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLessonsApplicationModule();
            services.BindConfiguration<DatabaseConfiguration>(configuration);

            services.AddDbContext<LessonsContext>();
            services.AddScoped<IPerformanceRepository, PerformanceRepository>();
            // services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
            return services;
        }

        public static WebApplication CreateLessonScheme(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<LessonsContext>();
            dbContext.Creator.EnsureCreated();
            return app;
        }
    }
}