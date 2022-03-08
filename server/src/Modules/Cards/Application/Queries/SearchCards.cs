using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
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
                    request.SearchingTerm,
                    request.SearchingDrawers,
                    request.LessonIncluded,
                    request.IsTicked,
                    request.PageNumber,
                    request.PageSize);

                var cards = await _queryRepository.SearchCards(searchQuery, cancellationToken);
                return cards;
            }
        }


        public class Query : IRequest<IEnumerable<CardSummary>>
        {
            public Guid OwnerId { get; set; }
            public string SearchingTerm { get; set; }
            public IEnumerable<int> SearchingDrawers { get; set; }
            public bool? LessonIncluded { get; set; }
            public bool? IsTicked { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
        }
    }
}