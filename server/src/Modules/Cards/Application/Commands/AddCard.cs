using System;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain;
using FluentValidation;

namespace Cards.Application.Commands
{
    public class AddCard
    {
        internal class CommandHandler : RequestHandlerBase<Command, Guid>
        {
            private readonly ISetRepository _repository;

            public CommandHandler(ISetRepository repository)
            {
                _repository = repository;
            }

            public async override Task<ResponseBase<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = UserId.Restore(request.UserId);
                var set = await _repository.Get(userId, cancellationToken);
                if (set is null) return ResponseBase<Guid>.Create("(set is null");

                var groupId = GroupId.Restore(request.GroupId);

                var frontLabel = SideLabel.Create(request.Front.Value);
                var backLabel = SideLabel.Create(request.Back.Value);

                var cardId = set.AddCard(
                    groupId,
                    frontLabel,
                    request.Front.Example,
                    request.Front.IsUsed,
                    backLabel,
                    request.Back.Example,
                    request.Back.IsUsed,
                    request.Comment);

                await _repository.Update(set, cancellationToken);
                return ResponseBase<Guid>.Create(cardId.Value);
            }
        }

        public class Command : RequestBase<Guid>
        {
            public Guid UserId { get; set; }
            public Guid GroupId { get; set; }
            public CardSide Front { get; set; }
            public CardSide Back { get; set; }
            public string Comment { get; set; }
        }

        public class CardSide
        {
            public string Value { get; set; }
            public string Example { get; set; }
            public bool IsUsed { get; set; }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.GroupId).Must(x => x != Guid.Empty);
                RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
                RuleFor(x => x.Front.Value).NotEmpty();
                RuleFor(x => x.Back.Value).NotEmpty();
            }
        }
    }
}