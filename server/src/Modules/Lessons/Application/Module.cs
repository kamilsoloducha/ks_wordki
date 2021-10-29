using Domain.IntegrationEvents;
using MassTransit.ExtensionsDependencyInjectionIntegration.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace Lessons.Application
{
    public static class LessonsApplicationModule
    {
        public static IServiceCollection AddLessonsApplicationModule(this IServiceCollection services)
        {
            return services;
        }

        public static void AddLessonsConsumers(this ServiceCollectionBusConfigurator configurator)
        {
            configurator.AddConsumer<UserCreatedConsumer>(typeof(UserCreatedConsumerDefinition)).Endpoint(e =>
            {
                e.Name = $"lessons-{nameof(UserCreated)}";
            });
        }
    }
}