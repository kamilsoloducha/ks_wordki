using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Cards;

public abstract class UpdateCard
{
    internal class CommandHandler : RequestHandlerBase<Command, Unit>
    {
        private readonly IOwnerRepository _repository;

        public CommandHandler(IOwnerRepository repository)
        {
            _repository = repository;
        }

        public override async Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = UserId.Restore(request.UserId);
            var card = await _repository.GetCard(ownerId, request.CardId, cancellationToken);


            var command = new Domain.Commands.UpdateCard(
                new Domain.Commands.Side(
                    new Label(request.Front.Value),
                    new Example(request.Front.Example),
                    request.Comment,
                    request.Front.IsUsed),
                new Domain.Commands.Side(
                    new Label(request.Back.Value),
                    new Example(request.Back.Example),
                    request.Comment,
                    request.Back.IsUsed),
                request.Front.IsTicked
            );

            card.Update(command);

            await _repository.Update(cancellationToken);
            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public record Command(
            Guid UserId,
            long CardId,
            CardSide Front,
            CardSide Back,
            string Comment)
        : IRequest<ResponseBase<Unit>>;

    public record CardSide(string Value, string Example, bool IsUsed, bool IsTicked);
}