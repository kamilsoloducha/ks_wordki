using System;
using System.Collections.Generic;
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

public class AddGroup
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
            var userId = OwnerId.Restore(request.UserId);
            var owner = await _repository.Get(userId, cancellationToken);
            if (owner is null) return ResponseBase<string>.Create("cardsSet is null");

            var groupName = GroupName.Create(request.GroupName);
            var front = Language.Create(request.Front);
            var back = Language.Create(request.Back);

            var groupId = owner.AddGroup(groupName, front, back, _sequenceGenerator);

            await _repository.Update(owner, cancellationToken);
            return ResponseBase<string>.Create(_hash.GetHash(groupId.Value));
        }
    }

    public class Command : RequestBase<string>
    {
        public Guid UserId { get; set; }
        public string GroupName { get; set; }
        public int Front { get; set; }
        public int Back { get; set; }
        public IEnumerable<Card> Cards { get; set; } = new Card[0];
    }

    public class Card
    {
        public string FrontValue { get; set; }
        public string FrontExample { get; set; }
        public string BackValue { get; set; }
        public string BackExample { get; set; }
        public string Comment { get; set; }
    }

    internal class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
            RuleFor(x => x.GroupName).NotEmpty();
            RuleFor(x => x.Front).Must(x => x >= 0);
            RuleFor(x => x.Back).Must(x => x >= 0);

            RuleForEach(x => x.Cards).Must(x => !string.IsNullOrWhiteSpace(x.FrontValue));
            RuleForEach(x => x.Cards).Must(x => !string.IsNullOrWhiteSpace(x.BackValue));
        }
    }
}