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
        internal class CommandHandler : RequestHandlerBase<Command, long>
        {
            private readonly IOwnerRepository _repository;
            private readonly ISequenceGenerator _sequenceGenerator;

            public CommandHandler(IOwnerRepository repository, ISequenceGenerator sequenceGenerator)
            {
                _repository = repository;
                _sequenceGenerator = sequenceGenerator;
            }

            public async override Task<ResponseBase<long>> Handle(Command request, CancellationToken cancellationToken)
            {
                var ownerId = OwnerId.Restore(request.UserId);
                var owner = await _repository.Get(ownerId, cancellationToken);
                if (owner is null) return ResponseBase<long>.Create("(set is null");

                var groupId = GroupId.Restore(request.GroupId);
                var frontValue = Label.Create(request.Front.Value);
                var backValue = Label.Create(request.Back.Value);
                var frontComment = Comment.Create(request.Comment);
                var backComment = Comment.Create(request.Comment);

                var cardId = owner.AddCard(
                    groupId,
                    frontValue,
                    backValue,
                    request.Front.Example,
                    request.Back.Example,
                    frontComment,
                    backComment,
                    _sequenceGenerator);

                await _repository.Update(owner, cancellationToken);
                return ResponseBase<long>.Create(cardId.Value);
            }
        }

        public class Command : RequestBase<long>
        {
            public Guid UserId { get; set; }
            public long GroupId { get; set; }
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
                RuleFor(x => x.GroupId).Must(x => x != 0);
                RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
                RuleFor(x => x.Front.Value).NotEmpty();
                RuleFor(x => x.Back.Value).NotEmpty();
            }
        }
    }
}