using System.Threading.Tasks;
using Cards.Domain.Enums;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using Domain.IntegrationEvents;
using MassTransit;
using MassTransit.Definition;

namespace Cards.Application.Consumers
{
    internal class AnswerRegisteredDefinition : ConsumerDefinition<AnswerRegisteredConsumer>
    {
        public AnswerRegisteredDefinition() { }
    }

    internal class AnswerRegisteredConsumer : IConsumer<AnswerRegistered>
    {
        private readonly INextRepeatCalculator _nextRepeatCalculator;
        private readonly IOwnerRepository _cardsRepository;


        public AnswerRegisteredConsumer(INextRepeatCalculator nextRepeatCalculator,
            IOwnerRepository cardsRepository)
        {
            _nextRepeatCalculator = nextRepeatCalculator;
            _cardsRepository = cardsRepository;
        }

        public async Task Consume(ConsumeContext<AnswerRegistered> context)
        {
            var userId = UserId.Restore(context.Message.UserId);

            var card = await _cardsRepository.GetCard(userId, context.Message.CardId, context.CancellationToken);
            var sideType = (SideType)context.Message.SideType;
        
            card.Register(sideType, context.Message.Result, _nextRepeatCalculator);

            await _cardsRepository.Update(context.CancellationToken);
        }
    }
}