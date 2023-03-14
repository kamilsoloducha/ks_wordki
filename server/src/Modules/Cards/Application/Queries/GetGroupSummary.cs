using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetGroupSummary
{
    internal class QueryHandler : IRequestHandler<Query, GroupSummaryDto>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }

        public async Task<GroupSummaryDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var group = await _queryRepository.GetGroup(request.UserId, request.GroupId, cancellationToken);

            return group is not null
                ? new GroupSummaryDto(_hash.GetHash(group.Id), group.Name.Text, group.Front, group.Back,
                    group.Cards.Count)
                : null;
        }
    }

    public record Query(Guid UserId, long GroupId) : IRequest<GroupSummaryDto>;
}