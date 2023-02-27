using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using Cards.Domain.ValueObjects;
using Domain.Utils;
using MediatR;

namespace Cards.Application.Queries;

public class GetRepeatsCount
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

            var ownerId = OwnerId.Restore(request.UserId);
            var now = SystemClock.Now.Date;

            var repeats = await _queryRepository.GetDailyRepeatsCount(ownerId, now, request.QuestionLanguage, cancellationToken);

            return repeats;
        }
    }

    public class Query : IRequest<int>
    {
        public Guid UserId { get; set; }
        public IEnumerable<int> QuestionLanguage { get; set; }
        public bool IsUsed { get; set; }
    }
}