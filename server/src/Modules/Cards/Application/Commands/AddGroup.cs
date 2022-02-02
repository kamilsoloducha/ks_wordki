using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Cards.Domain;
using FluentValidation;

namespace Cards.Application.Commands
{
    public class AddGroup
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
                var userId = OwnerId.Restore(request.UserId);
                var owner = await _repository.Get(userId, cancellationToken);
                if (owner is null) return ResponseBase<long>.Create("cardsSet is null");

                var groupName = GroupName.Create(request.GroupName);
                var front = Language.Create(request.Front);
                var back = Language.Create(request.Back);

                var groupId = owner.AddGroup(groupName, front, back, _sequenceGenerator);

                await _repository.Update(owner, cancellationToken);
                return ResponseBase<long>.Create(groupId.Value);
            }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {

        }

        public class Command : RequestBase<long>
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
    }
}