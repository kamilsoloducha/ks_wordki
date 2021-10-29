using System.Threading;
using System.Threading.Tasks;
using Cards.Domain;
using Domain.IntegrationEvents;
using MassTransit;

namespace Cards.Application
{
    internal class AnswerRegisterdConsumer : IConsumer<AnswerRegistered>
    {
        private readonly ISetRepository _repository;
        private readonly INextRepeatCalculator _nextRepeatCalculator;

        public AnswerRegisterdConsumer(ISetRepository repository,
        INextRepeatCalculator nextRepeatCalculator)
        {
            _repository = repository;
            _nextRepeatCalculator = nextRepeatCalculator;
        }

        public async Task Consume(ConsumeContext<AnswerRegistered> context)
        {
            var userId = UserId.Restore(context.Message.UserId);
            var set = await _repository.Get(userId, CancellationToken.None);

            var groupId = GroupId.Restore(context.Message.GroupId);
            var cardId = CardId.Restore(context.Message.CardId);
            var sideType = (Side)context.Message.Side;
            var result = context.Message.result;
            set.RegisterAnswer(groupId, cardId, sideType, result, _nextRepeatCalculator);

            await _repository.Update(set, CancellationToken.None);
        }
    }
}