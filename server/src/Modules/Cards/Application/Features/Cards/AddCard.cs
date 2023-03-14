using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.Commands;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Cards
{
    public abstract class AddCard
    {
        internal class CommandHandler : RequestHandlerBase<Command, long>
        {
            private readonly IOwnerRepository _repository;

            public CommandHandler(IOwnerRepository repository)
            {
                _repository = repository;
            }

            public override async Task<ResponseBase<long>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ownerId = UserId.Restore(request.UserId);

                var group = await _repository.GetGroup(ownerId, request.GroupId, cancellationToken);
                if (group is null) return ResponseBase<long>.CreateError("set is null");

            
                var addCardCommand = new AddCardCommand(
                    new Label(request.Front.Value),
                    new Label(request.Back.Value),
                    new Example(request.Front.Example),
                    new Example(request.Back.Example),
                    new Comment(request.Comment),
                    new Comment(request.Comment),
                    request.Front.IsUsed,
                    request.Back.IsUsed);
            
                var card = group.AddCard(addCardCommand);

                await _repository.Update(cancellationToken);
                return ResponseBase<long>.Create(card.Id);
            }
        }

        public record Command(Guid UserId, long GroupId, CardSide Front, CardSide Back, string Comment)
            : IRequest<ResponseBase<long>>;

        public record CardSide(string Value, string Example, bool IsUsed);
    }
}