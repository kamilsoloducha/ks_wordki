using Cards.Application;
using Cards.Application.Services;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Infrastructure.DataAccess;
using Cards.Infrastructure.Repository;
using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Infrastructure;

public static class Module
{
    public static IServiceCollection AddCardsInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCardsApplicationModule();

        services.AddOptions<DatabaseConfiguration>().Bind(configuration.GetSection(nameof(DatabaseConfiguration))).ValidateDataAnnotations();
        
        services.AddDbContext<CardsContext>();
        services.AddScoped<IOwnerRepository, CardsRepository>();
        services.AddScoped<IQueryRepository, QueryRepository>();
        services.AddScoped<ISequenceGenerator, DbSequenceGenerator>();

        return services;
    }
}