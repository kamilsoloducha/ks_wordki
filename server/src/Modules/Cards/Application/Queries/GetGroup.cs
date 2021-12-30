using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Queries
{

    public class GetGroup
    {
        public class Query : IRequest<Response>
        {
            public Guid UserId { get; set; }
            public Guid GroupId { get; set; }
        }

        internal class QueryHandler : IRequestHandler<Query, Response>
        {
            private readonly ISetRepository _repository;

            public QueryHandler(ISetRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = UserId.Restore(request.UserId);
                var cardsSet = await _repository.Get(userId, cancellationToken);
                var groupId = GroupId.Restore(request.GroupId);
                var group = cardsSet.GetGroup(groupId);

                return new Response
                {
                    Id = group.Id.Value,
                    Name = group.Name,
                    Front = group.FrontLanguage.Type,
                    Back = group.BackLanguage.Type,
                    Cards = group.Cards.OrderBy(x => x.CreationDate).Select(CreateCard)
                };
            }

            private Card CreateCard(Domain.Card card)
                => new Card
                {
                    Id = card.Id.Value,
                    Front = CreateCardSide(card.Front),
                    Back = CreateCardSide(card.Back),
                    Comment = card.Comment,
                    IsTicked = card.IsTicked
                };

            private CardSide CreateCardSide(Domain.CardSide side)
                => new CardSide
                {
                    Value = side.Value.Value,
                    Example = side.Example,
                    Drawer = side.Drawer.Value,
                    IsUsed = side.IsUsed
                };
        }

        public class Response
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public LanguageType Front { get; set; }
            public LanguageType Back { get; set; }
            public IEnumerable<Card> Cards { get; set; }
        }

        public class Card
        {
            public Guid Id { get; set; }
            public CardSide Front { get; set; }
            public CardSide Back { get; set; }
            public string Comment { get; set; }
            public bool IsTicked { get; set; }
        }
        public class CardSide
        {
            public string Value { get; set; }
            public string Example { get; set; }
            public int Drawer { get; set; }
            public bool IsUsed { get; set; }
        }
    }
}