using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Services;
using Cards.Domain;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Commands;

public class AppendGroup
{
    internal class CommandHandler : IRequestHandler<Command, string>
    {

        private readonly IOwnerRepository _ownerRepository;
        private readonly IQueryRepository _queryRepository;
        private readonly ISequenceGenerator _sequenceGenerator;
        private readonly IHashIdsService _hash;

        public CommandHandler(IOwnerRepository ownerRepository,
            IQueryRepository queryRepository,
            ISequenceGenerator sequenceGenerator, IHashIdsService hash)
        {
            _ownerRepository = ownerRepository;
            _queryRepository = queryRepository;
            _sequenceGenerator = sequenceGenerator;
            _hash = hash;
        }

        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.OwnerId);

            var owner = await _ownerRepository.Get(ownerId, cancellationToken);
            if (owner is null) throw new Exception("");

            var groupId = GroupId.Restore(_hash.GetLongId(request.GroupId));
            var group = await _ownerRepository.GetGroup(groupId, cancellationToken);

            var newGroupId = owner.AppendGroup(group, _sequenceGenerator);

            await _ownerRepository.Update(owner, cancellationToken);

            return _hash.GetHash(newGroupId.Value);
        }
    }

    public class Command : IRequest<string>
    {
        public Guid OwnerId { get; set; }
        public string GroupId { get; set; }
    }
}