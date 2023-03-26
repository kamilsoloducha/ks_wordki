using Cards.Application.Consumers;
using Cards.Domain;
using Domain.IntegrationEvents;
using MassTransit.ExtensionsDependencyInjectionIntegration.Registration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Application;

public static class Module
{
    public static IServiceCollection AddCardsApplicationModule(this IServiceCollection services)
    {
        services.AddCardDomainModule();
        services.AddMediatR(typeof(Module).Assembly);

        return services;
    }

    public static void AddCardsConsumers(this ServiceCollectionBusConfigurator configurator)
    {
        configurator.AddConsumer<AnswerRegisteredConsumer>(typeof(AnswerRegisteredDefinition)).Endpoint(e =>
        {
            e.Name = $"cards-{nameof(AnswerRegistered)}";
        });
        configurator.AddConsumer<UserCreatedConsumer>(typeof(UserCreatedDefinition)).Endpoint(e =>
        {
            e.Name = $"cards-{nameof(UserCreated)}";
        });
    }
}