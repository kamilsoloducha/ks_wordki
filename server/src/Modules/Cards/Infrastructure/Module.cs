using Cards.Application.Services;
using Cards.Domain;
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
        services.Configure<DatabaseConfiguration>(options => configuration.GetSection(nameof(DatabaseConfiguration)).Bind(options));

        services.AddDbContext<CardsContext>();
        services.AddScoped<IOwnerRepository, CardsRepository>();
        services.AddScoped<IQueryRepository, QueryRepository>();
        services.AddScoped<ISequenceGenerator, DbSequenceGenerator>();

        return services;
    }
}