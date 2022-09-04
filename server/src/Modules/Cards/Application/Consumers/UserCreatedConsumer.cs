using System.Threading;
using System.Threading.Tasks;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Domain.IntegrationEvents;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;

namespace Cards.Application.Consumers;

internal class UserCreatedDefinition : ConsumerDefinition<UserCreatedConsumer>
{
    public UserCreatedDefinition() { }
}

internal class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly ILogger<UserCreatedConsumer> _logger;
    private readonly IOwnerRepository _repository;

    public UserCreatedConsumer(ILogger<UserCreatedConsumer> logger,
        IOwnerRepository repository)
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