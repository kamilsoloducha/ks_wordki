using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries
{
    public class GetGroupsForLearn
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
                var groups = await _queryRepository.GetGroups(request.OwnerId, cancellationToken);
                return new Response
                {
                    Groups = groups,
                };
            }
        }

        public class Query : IRequest<Response>
        {
            public Guid OwnerId { get; set; }
        }

        public class Response
        {
            public IEnumerable<GroupToLesson> Groups { get; set; }
        }
    }
}