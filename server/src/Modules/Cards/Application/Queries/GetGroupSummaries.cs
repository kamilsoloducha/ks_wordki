using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Abstraction;
using Cards.Application.Queries.Models;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetGroupSummaries
{
    internal class QueryHandler : IRequestHandler<Query, IEnumerable<GroupSummaryDto>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }
        public async Task<IEnumerable<GroupSummaryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var groupsSummaries = await _queryRepository.GetGroupSummaries(request.OwnerId, cancellationToken);
            return groupsSummaries.Select(x => CreateDto(x, _hash)); 
        }

        private GroupSummaryDto CreateDto(GroupSummary groupSummary, IHashIdsService hashIds)
            => new(hashIds.GetHash(groupSummary.Id), groupSummary.Name, groupSummary.Front, groupSummary.Back,
                groupSummary.CardsCount);
    }

    public record Query(Guid OwnerId) : IRequest<IEnumerable<GroupSummaryDto>>;
}