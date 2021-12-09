using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Services;
using Cards.Application.Services;
using Cards.Domain;
using MediatR;
using Utils;

namespace Cards.Application.Queries
{
    public class GetDashboardSummary
    {
        internal class QueryHandler : IRequestHandler<Query, Response>
        {
            private readonly IQueryRepository _queryRepository;
            private readonly IUserDataProvider _userDataProvider;

            public QueryHandler(IQueryRepository queryRepository,
                IUserDataProvider userDataProvider)
            {
                _queryRepository = queryRepository;
                _userDataProvider = userDataProvider;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userIdValue = _userDataProvider.GetUserId();
                var userId = UserId.Restore(userIdValue);
                var dateTime = SystemClock.Now.Date;

                var dailyRepeats = await _queryRepository.GetDailyRepeatsCount(userId, dateTime, cancellationToken);
                var groupsCount = await _queryRepository.GetGroupsCount(userId, cancellationToken);
                var cardsCount = await _queryRepository.GetCardsCount(userId, cancellationToken);
                var repeatCounts = await _queryRepository.GetRepeatsCountSummary(userId, request.DateFrom, request.DateTo, cancellationToken);

                // var todayCount = repeatCounts.Where(x => x.Date <= dateTime).Aggregate((x1, x2) => new RepeatCount { Count = x1.Count + x2.Count, Date = dateTime, UserId = x2.UserId });
                // var allRepeatsCount = new List<RepeatCount>() { todayCount };
                // allRepeatsCount.AddRange(repeatCounts.Where(x => x.Date > dateTime));

                return new Response
                {
                    DailyRepeats = dailyRepeats,
                    GroupsCount = groupsCount,
                    CardsCount = cardsCount,
                    // RepeatsCounts = allRepeatsCount.Select(x => new RepeatsCount { Count = x.Count, Date = x.Date })
                };
            }
        }

        public class Query : IRequest<Response>
        {
            public DateTime DateFrom { get; set; } = new DateTime(1);
            public DateTime DateTo { get; set; } = new DateTime(3000, 1, 1);
        }

        public class Response
        {
            public int GroupsCount { get; set; }
            public int CardsCount { get; set; }
            public int DailyRepeats { get; set; }
            public IEnumerable<RepeatsCount> RepeatsCounts { get; set; }
        }

        public class RepeatsCount
        {
            public int Count { get; set; }
            public DateTime Date { get; set; }
        }
    }
}