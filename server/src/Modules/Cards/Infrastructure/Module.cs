using System;
using System.Reflection;
using Cards.Application;
using Cards.Application.Abstraction;
using Cards.Application.Abstraction.Dictionaries;
using Cards.Application.Services;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Infrastructure.DataAccess;
using Cards.Infrastructure.Implementations;
using Cards.Infrastructure.Implementations.Dictionaries;
using Cards.Infrastructure.Implementations.Dictionaries.Configuration;
using Cards.Infrastructure.Repository;
using FluentValidation;
using Infrastructure;
using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cards.Infrastructure;

public static class Module
{
    public static IServiceCollection AddCardsInfrastructureModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCardsApplicationModule();
        services.AddSingleton<IValidator<WordkiScrapperConfiguration>, WordkiScrapperConfigurationValidator>();
        services.AddSingleton<IValidator<ApiDictionaryConfiguration>, ApiDictionaryConfigurationValidator>();

        services.BindConfiguration<DatabaseConfiguration>(configuration);
        services.BindConfiguration<WordkiScrapperConfiguration>(configuration);
        services.BindConfiguration<ApiDictionaryConfiguration>(configuration);

        services.AddDbContext<CardsContext>();
        services.AddScoped<IOwnerRepository, CardsRepository>();
        services.AddScoped<IQueryRepository, QueryRepository>();
        services.AddScoped<ISequenceGenerator, DbSequenceGenerator>();
        
        services.AddHttpClient<IDictionary, ApiDevDictionary>("ApiDictionary",
            (provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<ApiDictionaryConfiguration>>();
                client.BaseAddress = new Uri(options.Value.Host);
            });
        
        services.AddHttpClient<IDictionary, CambridgeDictionary>("Cambridge",
            (provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<WordkiScrapperConfiguration>>();
                client.BaseAddress = new Uri(options.Value.Host);
            });
        
        services.AddHttpClient<IDictionary, DikiDictionary>("Diki",
            (provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<WordkiScrapperConfiguration>>();
                client.BaseAddress = new Uri(options.Value.Host);
            });

        services.AddKeyedScoped<IDictionary, ApiDevDictionary>("ApiDictionary");
        services.AddKeyedScoped<IDictionary, DikiDictionary>("Diki");
        services.AddKeyedScoped<IDictionary, CambridgeDictionary>("Cambridge");

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