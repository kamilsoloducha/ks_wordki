using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Abstraction;
using Cards.Application.Services;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Queries;

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
            var ownerId = UserId.Restore(request.UserId);
            long? groupId = string.IsNullOrEmpty(request.GroupId) ? null : _hash.GetLongId(request.GroupId);

            var repeats = await _queryRepository.GetNewRepeatsCount(ownerId, request.QuestionLanguage ?? string.Empty, groupId, cancellationToken);

            return repeats;
        }
    }

    public class Query : IRequest<int>
    {
        public Guid UserId { get; set; }
        public string GroupId { get; set; }
        public string QuestionLanguage { get; set; } = string.Empty;
    }

}