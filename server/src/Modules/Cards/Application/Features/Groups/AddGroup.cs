using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Groups;

public abstract class AddGroup
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
            var userId = OwnerId.Restore(request.UserId);
            var owner = await _repository.Get(userId, cancellationToken);
            if (owner is null) return ResponseBase<long>.CreateError("cardsSet is null");

            var groupName = GroupName.Create(request.GroupName);
            var front = Language.Create(request.Front);
            var back = Language.Create(request.Back);

            var groupId = owner.AddGroup(groupName, front, back, _sequenceGenerator);

            await _repository.Update(owner, cancellationToken);
            return ResponseBase<long>.Create(groupId.Value);
        }
    }

    public record Command(Guid UserId, string GroupName, int Front, int Back) : IRequest<ResponseBase<long>>;
}