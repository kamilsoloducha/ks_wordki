using System;
using System.Threading;
using System.Threading.Tasks;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Groups;

public abstract class AttachGroup
{
    internal class CommandHandler : IRequestHandler<Command, long>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ISequenceGenerator _sequenceGenerator;

        public CommandHandler(IOwnerRepository ownerRepository,
            ISequenceGenerator sequenceGenerator)
        {
            _ownerRepository = ownerRepository;
            _sequenceGenerator = sequenceGenerator;
        }

        public async Task<long> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.UserId);

            var owner = await _ownerRepository.Get(ownerId, cancellationToken);
            if (owner is null) throw new Exception("");

            var groupId = GroupId.Restore(request.GroupId);
            var group = await _ownerRepository.GetGroup(groupId, cancellationToken);

            var newGroupId = owner.AppendGroup(group, _sequenceGenerator);

            await _ownerRepository.Update(owner, cancellationToken);

            return newGroupId.Value;
        }
    }

    public record Command(Guid UserId, long GroupId) : IRequest<long>;
}