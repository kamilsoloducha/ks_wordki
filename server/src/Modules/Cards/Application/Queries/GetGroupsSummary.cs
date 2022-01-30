using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries
{

    public class GetGroupDetails
    {
        internal class QueryHandler : IRequestHandler<Query, Response>
        {
            private readonly IQueryRepository _queryRepository;

            public QueryHandler(IQueryRepository queryRepository)
            {
                _queryRepository = queryRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var groupDetails = await _queryRepository.GetGroupDetails(request.GroupId, cancellationToken);
                return new Response
                {
                    Id = groupDetails.Id,
                    Name = groupDetails.Name,
                    Front = groupDetails.Front,
                    Back = groupDetails.Back
                };
            }
        }
        public class Query : IRequest<Response>
        {
            public long GroupId { get; set; }
        }

        public class Response
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public int Front { get; set; }
            public int Back { get; set; }
        }
    }

    public class GetGroupsSummary
    {
        internal class QueryHandler : IRequestHandler<Query, Response>
        {
            private readonly IQueryRepository _queryRepository;

            public QueryHandler(IQueryRepository queryRepository)
            {
                _queryRepository = queryRepository;
            }
            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var groupsSummaries = await _queryRepository.GetGroupSummaries(request.OwnerId, cancellationToken);
                return new Response
                {
                    Groups = groupsSummaries
                };
            }
        }

        public class Query : IRequest<Response>
        {
            public Guid OwnerId { get; set; }
        }

        public class Response
        {
            public IEnumerable<GroupSummary> Groups { get; set; }
        }
    }
}