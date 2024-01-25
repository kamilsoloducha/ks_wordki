using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Abstraction;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public class SearchGroupsCount
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
            var query = SearchGroupsQuery.Create(request.SearchingTerm, request.PageNumber, request.PageSize);
            var count = await _queryRepository.GetGroupSummariesCount(query, cancellationToken);
            return count;
        }
    }
    public class Query : IRequest<int>
    {
        public string SearchingTerm { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}