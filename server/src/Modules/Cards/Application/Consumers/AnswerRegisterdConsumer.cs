using System.Threading;
using System.Threading.Tasks;
using Cards.Domain;
using Cards.Domain.Repositories;
using Domain.IntegrationEvents;
using MassTransit;
using MassTransit.Definition;

namespace Cards.Application
{
    internal class AnswerRegisteredDefinition : ConsumerDefinition<AnswerRegisterdConsumer>
    {
        public AnswerRegisteredDefinition() { }
    }

    internal class AnswerRegisterdConsumer : IConsumer<AnswerRegistered>
    {
        private readonly ISetRepository _repository;
        private readonly INextRepeatCalculator _nextRepeatCalculator;
        private readonly ICardRepository _cardRepository;

        public AnswerRegisterdConsumer(ISetRepository repository,
        INextRepeatCalculator nextRepeatCalculator,
        ICardRepository cardRepository)
        {
            _repository = repository;
            _nextRepeatCalculator = nextRepeatCalculator;
            _cardRepository = cardRepository;
        }

        public async Task Consume(ConsumeContext<AnswerRegistered> context)
        {
            var userId = UserId.Restore(context.Message.UserId);
            var cardId = CardId.Restore(context.Message.CardId);

            var card = await _cardRepository.GetCard(userId, cardId, CancellationToken.None);

            var sideType = (Side)context.Message.Side;
            var result = context.Message.Result;

            card.RegisterAnswer(sideType, result, _nextRepeatCalculator);

            await _cardRepository.Update(CancellationToken.None);
        }
    }
}