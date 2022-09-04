using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public class GetGroupsSummary
{
    internal class QueryHandler : IRequestHandler<Query, Response>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }
        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var groupsSummaries = await _queryRepository.GetGroupSummaries(request.OwnerId, cancellationToken);
            return new Response { Groups = groupsSummaries.Select(x => CreateDto(x, _hash)) };
        }

        private GroupSummaryDto CreateDto(GroupSummary groupSummary, IHashIdsService hashIds)
            => new GroupSummaryDto
            {
                Id = hashIds.GetHash(groupSummary.Id),
                Name = groupSummary.Name,
                Front = groupSummary.Front,
                Back = groupSummary.Back,
                CardsCount = groupSummary.CardsCount
            };
    }

    public class Query : IRequest<Response>
    {
        public Guid OwnerId { get; set; }
    }

    public class Response
    {
        public IEnumerable<GroupSummaryDto> Groups { get; set; }
    }

    public class GroupSummaryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Front { get; set; }
        public int Back { get; set; }
        public int CardsCount { get; set; }
    }
}