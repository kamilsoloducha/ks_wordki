using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Rabbit
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddHostedService<BusService>();
            services.AddMassTransit(x =>
            {
                x.UsingInMemory((context, config) =>
                {
                    config.TransportConcurrencyLimit = 10;
                    config.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
