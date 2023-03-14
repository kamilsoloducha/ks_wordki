using System;
using System.Threading;
using System.Threading.Tasks;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Features.Groups
{
    public abstract class AttachGroup
    {
        internal class CommandHandler : IRequestHandler<Command, long>
        {
            private readonly IOwnerRepository _ownerRepository;

            public CommandHandler(IOwnerRepository ownerRepository,
                ISequenceGenerator sequenceGenerator)
            {
                _ownerRepository = ownerRepository;
            }

            public async Task<long> Handle(Command request, CancellationToken cancellationToken)
            {
                var ownerId = UserId.Restore(request.UserId);

                var owner = await _ownerRepository.Get(ownerId, cancellationToken);
                if (owner is null) throw new Exception("");

                var group = await _ownerRepository.GetGroup(request.GroupId, cancellationToken);

                var newGroup = owner.UseGroup(group);

                await _ownerRepository.Update(cancellationToken);

                return newGroup.Id;
            }
        }

        public record Command(Guid UserId, long GroupId) : IRequest<long>;
    }
}