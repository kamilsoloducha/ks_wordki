using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.Commands;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Cards;

public abstract class AddCardExtenstion
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
            var owner = await _repository.Get(ownerId, cancellationToken);
            var chromeExtensionGroup = owner.Groups.FirstOrDefault(x => x.Name == GroupName.ChromeExtenstionGroupName);

            var group = chromeExtensionGroup ?? owner.CreateGroup(GroupName.ChromeExtenstionGroupName, "1", "2");
            var value = new Label(request.Value);

            var addCardCommand = new AddCardCommand(
                value,
                value,
                new Example(string.Empty),
                new Example(string.Empty),
                new Comment(string.Empty),
                new Comment(string.Empty),
                false, false);

            group.AddCard(addCardCommand);

            await _repository.Update(cancellationToken);
            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public record Command(Guid UserId, string Value) : IRequest<ResponseBase<Unit>>;
}