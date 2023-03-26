using System;
using Cards.Application;
using Cards.Application.Services;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Infrastructure.DataAccess;
using Cards.Infrastructure.Implementations;
using Cards.Infrastructure.Implementations.Dictionary;
using Cards.Infrastructure.Repository;
using Infrastructure;
using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Infrastructure;

public static class Module
{
    public static IServiceCollection AddCardsInfrastructureModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCardsApplicationModule();

        services.BindConfiguration<DatabaseConfiguration>(configuration);

        services.AddDbContext<CardsContext>();
        services.AddScoped<IOwnerRepository, CardsRepository>();
        services.AddScoped<IQueryRepository, QueryRepository>();
        services.AddScoped<ISequenceGenerator, DbSequenceGenerator>();
        services.AddHttpClient<IDictionary, DictionaryDevApi>(client =>
        {
            client.BaseAddress = new Uri("https://api.dictionaryapi.dev");
        });

        services.AddHttpClient<IDictionary, CambridgeDictionary>(client =>
        {
            client.BaseAddress = new Uri("https://dictionary.cambridge.org");
        });

        return services;
    }

    public static WebApplication CreateCardsScheme(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CardsContext>();
        dbContext.Creator.EnsureCreated();
        scope.Dispose();
        return app;
    }
}