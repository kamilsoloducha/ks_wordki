using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.Commands;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Cards;

public abstract class AddCard
{
    internal class CommandHandler : RequestHandlerBase<Command, long>
    {
        private readonly IOwnerRepository _repository;
        private readonly ISequenceGenerator _sequenceGenerator;

        public CommandHandler(IOwnerRepository repository, ISequenceGenerator sequenceGenerator)
        {
            _repository = repository;
            _sequenceGenerator = sequenceGenerator;
        }

        public override async Task<ResponseBase<long>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.UserId);

            var owner = await _repository.Get(ownerId, cancellationToken);
            if (owner is null) return ResponseBase<long>.CreateError("set is null");

            var addCardCommand = new AddCardCommand(
                GroupId.Restore(request.GroupId),
                Label.Create(request.Front.Value),
                Label.Create(request.Back.Value),
                new Example(request.Front.Example),
                new Example(request.Back.Example),
                Comment.Create(request.Comment),
                Comment.Create(request.Comment),
                request.Front.IsUsed,
                request.Back.IsUsed);

            var cardId = owner.AddCard(addCardCommand, _sequenceGenerator);

            await _repository.Update(owner, cancellationToken);
            return ResponseBase<long>.Create(cardId.Value);
        }
    }

    public record Command(Guid UserId, long GroupId, CardSide Front, CardSide Back, string Comment)
        : IRequest<ResponseBase<long>>;

    public record CardSide(string Value, string Example, bool IsUsed);
}