using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
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
            private readonly IHashIdsService _hash;

            public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
            {
                _queryRepository = queryRepository;
                _hash = hash;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                var ownerId = OwnerId.Restore(request.UserId);
                long? groupId = string.IsNullOrEmpty(request.GroupId) ? null : _hash.GetLongId(request.GroupId);

                var repeats = await _queryRepository.GetNewRepeatsCount(ownerId, request.QuestionLanguage ?? 0, groupId, cancellationToken);

                return repeats;
            }
        }

        public class Query : IRequest<int>
        {
            public Guid UserId { get; set; }
            public string GroupId { get; set; }
            public int? QuestionLanguage { get; set; }
        }

    }
}