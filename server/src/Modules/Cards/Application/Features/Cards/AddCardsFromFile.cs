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
            var ownerId = UserId.Restore(request.UserId);

            var group = await _repository.GetGroup(ownerId, request.GroupId, cancellationToken);

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
                    new Label(frontValue),
                    new Label(backValue),
                    new Example(frontExample),
                    new Example(backExample),
                    new Comment(string.Empty),
                    new Comment(string.Empty),
                    false, false);
                
                group.AddCard(addCardCommand);
            }

            await _repository.Update(cancellationToken);

            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public record Command(Guid UserId, long GroupId, string Content, string ItemSeparator, string ElementSeparator,
        string[] ItemsOrder) : IRequest<ResponseBase<Unit>>;
}