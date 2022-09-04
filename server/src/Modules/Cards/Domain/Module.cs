using System.Runtime.CompilerServices;
using Cards.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Cards.Domain.Tests")]
namespace Cards.Domain;

public static class Module
{
    public static IServiceCollection AddCardDomainModule(this IServiceCollection services)
    {
        services.AddScoped<INextRepeatCalculator, StandartCalculator>();
        return services;
    }
}