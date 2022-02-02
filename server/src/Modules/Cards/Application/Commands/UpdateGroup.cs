using System;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain;
using FluentValidation;
using MediatR;

namespace Cards.Application.Commands
{
    public class UpdateGroup
    {
        internal class CommandHandler : RequestHandlerBase<Command, Unit>
        {
            private readonly IOwnerRepository _repository;

            public CommandHandler(IOwnerRepository repository)
            {
                _repository = repository;
            }

            public async override Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ownerId = OwnerId.Restore(request.UserId);
                var owner = await _repository.Get(ownerId, cancellationToken);
                if (owner is null) return ResponseBase<Unit>.Create("set is null");

                var groupId = GroupId.Restore(request.GroupId);
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
            public long GroupId { get; set; }
            public string GroupName { get; set; }
            public int Front { get; set; }
            public int Back { get; set; }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.GroupId).Must(x => x != 0);
                RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
                RuleFor(x => x.GroupName).NotEmpty();
            }
        }
    }
}