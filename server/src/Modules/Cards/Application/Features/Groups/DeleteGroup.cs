using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Groups;

public abstract class DeleteGroup
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
            var ownerId = OwnerId.Restore(request.UserId);
            var owner = await _repository.Get(ownerId, cancellationToken);

            var groupId = GroupId.Restore(request.GroupId);

            owner.RemoveGroup(groupId);

            await _repository.Update(owner, cancellationToken);

            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public record Command(Guid UserId, long GroupId) : IRequest<ResponseBase<Unit>>;
}