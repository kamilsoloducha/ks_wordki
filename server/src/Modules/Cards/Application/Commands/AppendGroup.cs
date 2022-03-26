using System;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Commands
{
    public class AppendGroup
    {
        internal class CommandHandler : IRequestHandler<Command, long>
        {

            private IOwnerRepository _ownerRepository;
            private IQueryRepository _queryRepository;
            private ISequenceGenerator _sequenceGenerator;

            public CommandHandler(IOwnerRepository ownerRepository,
                IQueryRepository queryRepository,
                ISequenceGenerator sequenceGenerator)
            {
                _ownerRepository = ownerRepository;
                _queryRepository = queryRepository;
                _sequenceGenerator = sequenceGenerator;
            }

            public async Task<long> Handle(Command request, CancellationToken cancellationToken)
            {
                var ownerId = OwnerId.Restore(request.OwnerId);

                var owner = await _ownerRepository.Get(ownerId, cancellationToken);
                if (owner is null) throw new Exception("");

                var groupId = GroupId.Restore(request.GroupId);
                var group = await _ownerRepository.GetGroup(groupId, cancellationToken);

                var newGroupId = owner.AppendGroup(group, _sequenceGenerator);

                await _ownerRepository.Update(owner, cancellationToken);

                return newGroupId.Value;
            }
        }

        public class Command : IRequest<long>
        {
            public Guid OwnerId { get; set; }
            public long GroupId { get; set; }
        }
    }
}