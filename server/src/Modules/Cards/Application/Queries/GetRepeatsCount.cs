using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using Cards.Domain;
using MediatR;
using Utils;

namespace Cards.Application.Queries
{
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
                if (!request.QuestionLanguage.HasValue)
                {
                    throw new Exception($"{nameof(request.QuestionLanguage)} is not defined");
                }
                // todo move validation to fluentValidation

                var ownerId = OwnerId.Restore(request.UserId);
                var now = SystemClock.Now.Date;

                var repeats = await _queryRepository.GetDailyRepeatsCount(ownerId, now, request.QuestionLanguage.Value, cancellationToken);

                return repeats;
            }
        }

        public class Query : IRequest<int>
        {
            public Guid UserId { get; set; }
            public int? QuestionLanguage { get; set; }
            public IEnumerable<Guid> GroupIds { get; set; }
            public bool IsUsed { get; set; }
        }
    }
}