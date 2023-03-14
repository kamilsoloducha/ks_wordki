using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Domain.Utils;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetForecast
{
    internal sealed class QueryHandler : IRequestHandler<Query, IEnumerable<RepeatCount>>
    {
        private readonly IQueryRepository _queryRepository;

        public QueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<IEnumerable<RepeatCount>> Handle(Query request, CancellationToken cancellationToken)
        {
            var (startDate, stopDate) = GetPeriod(request.Count);

            var data = await _queryRepository.GetRepeatsPerDay(request.OwnerId, startDate, stopDate, cancellationToken);

            return FillGaps(data.ToList(), startDate, request.Count);
        }

        private static (DateTime, DateTime) GetPeriod(int count)
        {
            var startDate = SystemClock.Now.Date.AddDays(1);
            var stopDate = startDate.AddDays(count);
            return (startDate, stopDate);
        }

        private static IEnumerable<RepeatCount> FillGaps(IList<RepeatCount> dbDate, DateTime startDate, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var consideringDate = startDate.AddDays(i);
                yield return dbDate.FirstOrDefault(x => x.Date == consideringDate) ??
                             new RepeatCount(0, consideringDate);
            }
        }
    }

    public record Query(Guid OwnerId, int Count) : IRequest<IEnumerable<RepeatCount>>;
}