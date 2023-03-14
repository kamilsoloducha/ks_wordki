using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace Application.MassTransit
{
    public static class Extensions
    {
        public static async Task PublishMany<T>(this IPublishEndpoint endpoint, IEnumerable<T> events,
            CancellationToken cancellationToken)
            where T : class
        {
            foreach (var @event in events) await endpoint.Publish(@event, cancellationToken);
        }
    }
}