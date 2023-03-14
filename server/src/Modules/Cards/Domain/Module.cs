using Cards.Domain.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Cards.Domain
{
    public static class Module
    {
        public static IServiceCollection AddCardDomainModule(this IServiceCollection services)
        {
            services.AddScoped<INextRepeatCalculator, StandartCalculator>();
            return services;
        }
    }
}