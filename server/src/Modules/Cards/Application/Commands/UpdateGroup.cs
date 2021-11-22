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
            private readonly ISetRepository _repository;

            public CommandHandler(ISetRepository repository)
            {
                _repository = repository;
            }

            public async override Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = UserId.Restore(request.UserId);
                var set = await _repository.Get(userId, cancellationToken);
                if (set is null) return ResponseBase<Unit>.Create("set is null");

                var groupId = GroupId.Restore(request.GroupId);

                var group = set.GetGroup(groupId);

                var groupName = GroupName.Create(request.GroupName);

                group.Update(groupName, request.Front, request.Back);

                await _repository.Update(set, cancellationToken);
                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid UserId { get; set; }
            public Guid GroupId { get; set; }
            public string GroupName { get; set; }
            public LanguageType Front { get; set; }
            public LanguageType Back { get; set; }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.GroupId).Must(x => x != Guid.Empty);
                RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
                RuleFor(x => x.GroupName).NotEmpty();
            }
        }
    }
}