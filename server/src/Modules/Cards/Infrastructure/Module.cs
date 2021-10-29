using System;
using System.Threading.Tasks;
using Blueprints.Infrastructure.DataAccess;
using Cards.Application.Services;
using Cards.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Infrastructure
{
    public static class Module
    {
        public static IServiceCollection AddCardsInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfiguration>(options => configuration.GetSection(nameof(DatabaseConfiguration)).Bind(options));

            services.AddDbContext<CardsContext>();
            services.AddScoped<ISetRepository, SetRepository>();
            services.AddScoped<IQueryRepository, QueryRepository>();
            services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();

            return services;
        }

        public static async Task CreateCardsDb(this IServiceProvider services)
        {
            var cardsContext = services.GetService<CardsContext>();
            var creator = cardsContext.Creator;
            await creator.CreateTablesAsync();
        }
    }
}