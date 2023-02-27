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

public abstract class AddCardsFromFile
{
    internal class CommandHandler : RequestHandlerBase<Command, Unit>
    {
        private readonly IOwnerRepository _repository;
        private readonly ISequenceGenerator _sequenceGenerator;

        public CommandHandler(IOwnerRepository repository,
            ISequenceGenerator sequenceGenerator)
        {
            _repository = repository;
            _sequenceGenerator = sequenceGenerator;
        }

        public override async Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.UserId);
            var groupId = GroupId.Restore(request.GroupId);

            var owner = await _repository.Get(ownerId, cancellationToken);

            var itemLines = request.Content.Split(request.ItemSeparator);
            foreach (var itemLine in itemLines)
            {
                var elements = itemLine.Split(request.ElementSeparator);

                var frontValueIndex = Array.IndexOf(request.ItemsOrder, "FV");
                var frontExampleIndex = Array.IndexOf(request.ItemsOrder, "FE");
                var backValueIndex = Array.IndexOf(request.ItemsOrder, "BV");
                var backExampleIndex = Array.IndexOf(request.ItemsOrder, "BE");

                var frontValue = elements[frontValueIndex];
                var backValue = elements[backValueIndex];

                var frontExample = frontExampleIndex >= 0 ? elements[frontExampleIndex] : string.Empty;
                var backExample = backExampleIndex >= 0 ? elements[backExampleIndex] : string.Empty;

                var addCardCommand = new AddCardCommand(
                    groupId,
                    Label.Create(frontValue),
                    Label.Create(backValue),
                    new Example(frontExample),
                    new Example(backExample),
                    Comment.Create(string.Empty),
                    Comment.Create(string.Empty),
                    false, false);

                owner.AddCard(addCardCommand, _sequenceGenerator);
            }

            await _repository.Update(owner, cancellationToken);

            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public record Command(Guid UserId, long GroupId, string Content, string ItemSeparator, string ElementSeparator,
        string[] ItemsOrder) : IRequest<ResponseBase<Unit>>;
}