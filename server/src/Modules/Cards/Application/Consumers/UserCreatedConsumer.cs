using System.Threading;
using System.Threading.Tasks;
using Cards.Domain2;
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
        private readonly ICardsRepository _repository;

        public UserCreatedConsumer(ILogger<UserCreatedConsumer> logger,
        ICardsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var ownerId = OwnerId.Restore(context.Message.Id);
            var owner = Owner.Restore(ownerId);
            await _repository.Add(owner, CancellationToken.None);
        }
    }
}