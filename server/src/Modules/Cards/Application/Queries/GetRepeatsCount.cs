using System;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Abstraction;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
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
            var ownerId = UserId.Restore(request.UserId);

            var repeats = await _queryRepository.GetDailyRepeatsCount(ownerId, RepeatPeriod.To,
                request.QuestionLanguage, cancellationToken);

            return repeats;
        }
    }

    public record Query(Guid UserId, string[] QuestionLanguage) : IRequest<int>;
}