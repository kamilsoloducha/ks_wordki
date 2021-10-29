using System.Threading.Tasks;
using Domain.IntegrationEvents;
using Lessons.Domain;
using MassTransit;
using MassTransit.Definition;

namespace Lessons.Application
{
    internal class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IPerformanceRepository _repository;

        public UserCreatedConsumer(IPerformanceRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var userId = context.Message.Id;
            var newPerformance = Performance.Create(userId);

            await _repository.Add(newPerformance);
        }
    }

    internal class UserCreatedConsumerDefinition : ConsumerDefinition<UserCreatedConsumer>
    {

    }
}