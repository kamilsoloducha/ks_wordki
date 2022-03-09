using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Queries
{
    public class SearchCards
    {
        internal class QueryHandler : IRequestHandler<Query, IEnumerable<CardSummary>>
        {

            private readonly IQueryRepository _queryRepository;

            public QueryHandler(IQueryRepository queryRepository)
            {
                _queryRepository = queryRepository;
            }

            public async Task<IEnumerable<CardSummary>> Handle(Query request, CancellationToken cancellationToken)
            {
                var searchQuery = SearchCardsQuery.Create(
                    request.OwnerId,
                    request.SearchingTerm,
                    request.SearchingDrawers,
                    request.LessonIncluded,
                    request.OnlyTicked,
                    request.PageNumber,
                    request.PageSize);

                var cards = await _queryRepository.SearchCards(searchQuery, cancellationToken);
                return cards.Select(ToDto);
            }

            private SearchCards.CardSummary ToDto(Cards.Application.Queries.Models.CardSummary card)
            {
                return new SearchCards.CardSummary
                {
                    Id = card.CardId,
                    Front = new SearchCards.SideSummary
                    {
                        Type = (int)SideType.Front,
                        Value = card.FrontValue,
                        Example = card.FrontExample,
                        Comment = card.FrontDetailsComment,
                        Drawer = Math.Min(card.FrontDrawer + 1, 5),
                        IsUsed = card.FrontLessonIncluded,
                        IsTicked = card.FrontIsTicked,
                    },
                    Back = new SearchCards.SideSummary
                    {
                        Type = (int)SideType.Back,
                        Value = card.BackValue,
                        Example = card.BackExample,
                        Comment = card.BackDetailsComment,
                        Drawer = Math.Min(card.BackDrawer + 1, 5),
                        IsUsed = card.BackLessonIncluded,
                        IsTicked = card.BackIsTicked,
                    },
                };
            }
        }

        public class Query : IRequest<IEnumerable<CardSummary>>
        {
            public Guid OwnerId { get; set; }
            public string SearchingTerm { get; set; }
            public IEnumerable<int> SearchingDrawers { get; set; }
            public bool? LessonIncluded { get; set; }
            public bool OnlyTicked { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
        }

        public class CardSummary
        {
            public long Id { get; set; }
            public SideSummary Back { get; set; }
            public SideSummary Front { get; set; }
        }

        public class SideSummary
        {
            public int Type { get; set; }
            public string Value { get; set; }
            public string Example { get; set; }
            public string Comment { get; set; }
            public int Drawer { get; set; }
            public bool IsUsed { get; set; }
            public bool IsTicked { get; set; }
        }


    }
}