using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Commands
{
    public class AddCardSide
    {
        public class CommandHandler : RequestHandlerBase<Command, Unit>
        {
            private readonly ISetRepository _repository;

            public CommandHandler(ISetRepository repository)
            {
                _repository = repository;
            }

            public async override Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = UserId.Restore(request.UserId);
                var set = await _repository.Get(userId, cancellationToken);

                foreach (var item in request.CardSideIds)
                {
                    var cardId = CardId.Restore(item.CardId);
                    set.ChangeUsage(cardId, item.Side);
                }

                await _repository.Update(set, cancellationToken);

                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid UserId { get; set; }
            public IEnumerable<CardSideId> CardSideIds { get; set; }
        }

        public class CardSideId
        {
            public Guid CardId { get; set; }
            public Side Side { get; set; }
        }
    }
}