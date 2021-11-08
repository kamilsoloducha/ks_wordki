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
                var cardsSet = await _repository.Get(userId, cancellationToken);
                if (cardsSet is null) return ResponseBase<Guid>.Create("cardsSet is null");

                var groupId = cardsSet.AddGroup(request.GroupName, request.Front, request.Back);

                foreach (var item in request.Cards)
                {
                    cardsSet.AddCard(groupId,
                        item.FrontValue,
                        item.FrontExample,
                        false,
                        item.BackValue,
                        item.BackExample,
                        false,
                        item.Comment);
                }

                await _repository.Update(cardsSet, cancellationToken);
                return ResponseBase<Guid>.Create(groupId.Value);
            }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {

        }

        public class Command : RequestBase<Guid>
        {
            public Guid UserId { get; set; }
            public string GroupName { get; set; }
            public LanguageType Front { get; set; }
            public LanguageType Back { get; set; }
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