using System;
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
                var ownerId = OwnerId.Restore(request.UserId);

                var repeats = await _queryRepository.GetNewRepeatsCount(ownerId, request.QuestionLanguage ?? 0, request.GroupId.Value, cancellationToken);

                return repeats;
            }
        }

        public class Query : IRequest<int>
        {
            public Guid UserId { get; set; }
            public long? GroupId { get; set; }
            public int? QuestionLanguage { get; set; }
        }

    }
}