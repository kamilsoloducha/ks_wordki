using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Application.Services;
using Cards.Domain;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FluentValidation;

namespace Cards.Application.Commands;

public class AddCard
{
    internal class CommandHandler : RequestHandlerBase<Command, string>
    {
        private readonly IOwnerRepository _repository;
        private readonly ISequenceGenerator _sequenceGenerator;
        private readonly IHashIdsService _hash;

        public CommandHandler(IOwnerRepository repository, ISequenceGenerator sequenceGenerator, IHashIdsService hash)
        {
            _repository = repository;
            _sequenceGenerator = sequenceGenerator;
            _hash = hash;
        }

        public override async Task<ResponseBase<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ownerId = OwnerId.Restore(request.UserId);

            var owner = await _repository.Get(ownerId, cancellationToken);
            if (owner is null) return ResponseBase<string>.Create("set is null");

            var groupId = GroupId.Restore(_hash.GetLongId(request.GroupId));
            var frontValue = Label.Create(request.Front.Value);
            var backValue = Label.Create(request.Back.Value);
            var frontExample = new Example(request.Front.Example);
            var backExample = new Example(request.Back.Example);
            var frontComment = Comment.Create(request.Comment);
            var backComment = Comment.Create(request.Comment);

            var cardId = owner.AddCard(
                groupId,
                frontValue,
                backValue,
                frontExample,
                backExample,
                frontComment,
                backComment,
                _sequenceGenerator);

            await _repository.Update(owner, cancellationToken);
            return ResponseBase<string>.Create(_hash.GetHash(cardId.Value));
        }
    }

    public class Command : RequestBase<string>
    {
        public Guid UserId { get; set; }
        public string GroupId { get; set; }
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
            RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
            RuleFor(x => x.GroupId).NotEmpty();
            RuleFor(x => x.Front.Value).NotEmpty();
            RuleFor(x => x.Back.Value).NotEmpty();
        }
    }
}