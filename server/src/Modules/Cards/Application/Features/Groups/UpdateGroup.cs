using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Groups;

public abstract class UpdateGroup
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
            var group = await _repository.GetGroup(ownerId, request.GroupId, cancellationToken);

            if (group is null)
            {
                return ResponseBase<Unit>.CreateError("Group is not found");
            }

            group.Name = new GroupName(request.Name);
            group.Front = request.Front;
            group.Back = request.Back;

            await _repository.Update(cancellationToken);
            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public record Command
        (Guid UserId, long GroupId, string Name, string Front, string Back) : IRequest<ResponseBase<Unit>>;
}