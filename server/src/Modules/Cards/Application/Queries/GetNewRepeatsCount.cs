using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using Cards.Domain;
using MediatR;

namespace Cards.Application.Queries
{
    public class GetNewRepeatsCount
    {
        public class QueryHandler : IRequestHandler<Query, int>
        {
            private readonly IQueryRepository _queryRepository;

            public QueryHandler(IQueryRepository queryRepository)
            {
                _queryRepository = queryRepository;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = UserId.Restore(request.UserId);

                var repeats = await _queryRepository.GetNewRepeatsCount(userId, request.QuestionLanguage ?? 0, request.GroupId, cancellationToken);

                return repeats;
            }
        }

        public class Query : IRequest<int>
        {
            public Guid UserId { get; set; }
            public Guid? GroupId { get; set; }
            public int? QuestionLanguage { get; set; }
        }

    }
}