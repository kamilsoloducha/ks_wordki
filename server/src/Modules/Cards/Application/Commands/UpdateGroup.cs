using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Application.Services;
using Cards.Domain;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Cards.Application.Commands;

public class UpdateGroup
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

        public override async Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.UserId);
            var owner = await _repository.Get(ownerId, cancellationToken);

            var groupId = GroupId.Restore(_hash.GetLongId(request.GroupId));
            var groupName = GroupName.Create(request.GroupName);
            var front = Language.Create(request.Front);
            var back = Language.Create(request.Back);

            owner.UpdateGroup(groupId, groupName, front, back);

            await _repository.Update(owner, cancellationToken);
            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public class Command : RequestBase<Unit>
    {
        public Guid UserId { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public int Front { get; set; }
        public int Back { get; set; }
    }

    internal class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.GroupId).NotNull();
            RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
            RuleFor(x => x.GroupName).NotEmpty();
        }
    }
}