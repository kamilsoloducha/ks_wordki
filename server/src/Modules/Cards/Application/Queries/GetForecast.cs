using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public class GetForecast
{
    internal class QueryHandler : IRequestHandler<Query, IEnumerable<RepeatCount>>
    {
        private readonly IQueryRepository _queryRepository;

        public QueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<IEnumerable<RepeatCount>> Handle(Query request, CancellationToken cancellationToken)
        {
            var stop = request.StartDate.AddDays(request.Count);
            var result = new List<RepeatCount>();
            for (int i = 0; i < request.Count; i++)
            {
                result.Add(new RepeatCount { Count = 0, Date = request.StartDate.AddDays(i).Date });
            }

            var data = await _queryRepository.GetRepeatsPerDay(request.OwnerId, request.StartDate, stop, cancellationToken);
            for (int i = 0; i < request.Count; i++)
            {
                var item = data.FirstOrDefault(x => x.Date == result[i].Date);
                if (item == null) continue;
                result[i].Count = item.Count;
            }
            return result;
        }
    }

    public class Query : IRequest<IEnumerable<RepeatCount>>
    {
        public Guid OwnerId { get; set; }
        public DateTime StartDate { get; set; }
        public int Count { get; set; }
    }
}