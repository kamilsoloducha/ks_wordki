using System.Threading;
using System.Threading.Tasks;
using Cards.Domain;
using Domain.IntegrationEvents;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;

namespace Cards.Application
{
    internal class UserCreatedDefinition : ConsumerDefinition<UserCreatedConsumer>
    {
        public UserCreatedDefinition() { }
    }

    internal class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly ILogger<UserCreatedConsumer> _logger;
        private readonly ISetRepository _repository;

        public UserCreatedConsumer(ILogger<UserCreatedConsumer> logger,
        ISetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var userId = UserId.Restore(context.Message.Id);
            var cardsSet = Set.Create(userId);
            await _repository.Add(cardsSet, CancellationToken.None);
        }
    }
}