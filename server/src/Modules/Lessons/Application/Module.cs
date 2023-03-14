using Domain.IntegrationEvents;
using Lessons.Application.Consumers;
using MassTransit.ExtensionsDependencyInjectionIntegration.Registration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Lessons.Application
{
    public static class LessonsApplicationModule
    {
        public static IServiceCollection AddLessonsApplicationModule(this IServiceCollection services)
        {
            services.AddMediatR(typeof(LessonsApplicationModule).Assembly);
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