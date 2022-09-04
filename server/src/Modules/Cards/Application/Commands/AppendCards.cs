using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Application.Services;
using Cards.Domain;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Commands;

public class AppendCards
{
    internal class CommandHandler : RequestHandlerBase<Command, Unit>
    {

        private readonly IOwnerRepository _repository;
        private readonly IHashIdsService _hash;

        public CommandHandler(IOwnerRepository repository, IHashIdsService hash)
        {
            _repository = repository;
            _hash = hash;
        }

        public async override Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.OwnerId);
            var groupId = GroupId.Restore(_hash.GetLongId(request.GroupId));

            var owner = await _repository.Get(ownerId, cancellationToken);
            owner.IncludeToLesson(groupId, request.Count, request.Langauges);

            await _repository.Update(owner, cancellationToken);

            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public class Command : RequestBase<Unit>
    {
        public Guid OwnerId { get; set; }
        public string GroupId { get; set; }
        public int Count { get; set; }
        public int Langauges { get; set; }
    }
}