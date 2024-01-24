using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Abstraction;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public abstract class SearchCardsCount
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
                request.UserId,
                request.SearchingTerm,
                request.SearchingDrawers,
                request.LessonIncluded,
                request.Ticked);

            var count = await _queryRepository.SearchCardsCount(searchQuery, cancellationToken);
            return count;
        }

    }
    public record Query(
        Guid UserId,
        string SearchingTerm,
        IEnumerable<int> SearchingDrawers,
        bool? LessonIncluded,
        bool? Ticked) : IRequest<int>;
}