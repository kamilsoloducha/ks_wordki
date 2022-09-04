using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public class SearchCardsCount
{
    internal class QueryHandler : IRequestHandler<Query, int>
    {
        private readonly IQueryRepository _queryRepository;

        public QueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<int> Handle(Query request, CancellationToken cancellationToken)
        {
            var searchQuery = SearchCardsQuery.Create(
                request.OwnerId,
                request.SearchingTerm,
                request.SearchingDrawers,
                request.LessonIncluded,
                request.OnlyTicked,
                request.PageNumber,
                request.PageSize);

            var count = await _queryRepository.SearchCardsCount(searchQuery, cancellationToken);
            return count;
        }

    }

    public class Query : IRequest<int>
    {
        public Guid OwnerId { get; set; }
        public string SearchingTerm { get; set; }
        public IEnumerable<int> SearchingDrawers { get; set; }
        public bool? LessonIncluded { get; set; }
        public bool OnlyTicked { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }


}