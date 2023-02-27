using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.Commands;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Commands;

public abstract class AddCardExtenstion
{
    internal class CommandHandler : RequestHandlerBase<Command, Unit>
    {
        private readonly IOwnerRepository _repository;
        private readonly ISequenceGenerator _sequenceGenerator;

        public CommandHandler(
            IOwnerRepository repository,
            ISequenceGenerator sequenceGenerator)
        {
            _repository = repository;
            _sequenceGenerator = sequenceGenerator;
        }

        public override async Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.UserId);
            var owner = await _repository.Get(ownerId, cancellationToken);
            var chromeExtensionGroup = owner.Groups.FirstOrDefault(x => x.Name == GroupName.ChromeExtenstionGroupName);

            var groupId = chromeExtensionGroup?.Id ?? owner.AddGroup(GroupName.ChromeExtenstionGroupName,
                Language.Create(1), Language.Create(2), _sequenceGenerator);
            var value = Label.Create(request.Value);
            
            var addCardCommand = new AddCardCommand(
                groupId,
                value,
                value,
                new Example(string.Empty),
                new Example(string.Empty),
                Comment.Create(string.Empty),
                Comment.Create(string.Empty),
                false,false);
            
            owner.AddCard(addCardCommand, _sequenceGenerator);

            await _repository.Update(owner, cancellationToken);
            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public record Command(Guid UserId, string Value) : IRequest<ResponseBase<Unit>>;
}