using System.Threading;
using System.Threading.Tasks;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using Domain.IntegrationEvents;
using MassTransit;
using MassTransit.Definition;

namespace Cards.Application.Consumers;

internal class AnswerRegisteredDefinition : ConsumerDefinition<AnswerRegisterdConsumer>
{
    public AnswerRegisteredDefinition() { }
}

internal class AnswerRegisterdConsumer : IConsumer<AnswerRegistered>
{
    private readonly INextRepeatCalculator _nextRepeatCalculator;
    private readonly IOwnerRepository _cardsRepository;


    public AnswerRegisterdConsumer(INextRepeatCalculator nextRepeatCalculator,
        IOwnerRepository cardsRepository)
    {
        _nextRepeatCalculator = nextRepeatCalculator;
        _cardsRepository = cardsRepository;
    }

    public async Task Consume(ConsumeContext<AnswerRegistered> context)
    {
        var userId = OwnerId.Restore(context.Message.UserId);
        var sideId = SideId.Restore(context.Message.SideId);

        var detail = await _cardsRepository.Get(userId, sideId, CancellationToken.None);
        var result = context.Message.Result;

        detail.RegisterAnswer(result, _nextRepeatCalculator);

        await _cardsRepository.Update(CancellationToken.None);
    }
}