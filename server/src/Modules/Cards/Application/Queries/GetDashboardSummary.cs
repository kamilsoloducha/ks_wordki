using System;
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

                return new Response
                {
                    DailyRepeats = dailyRepeats,
                    GroupsCount = groupsCount,
                    CardsCount = cardsCount
                };
            }
        }

        public class Query : IRequest<Response> { }

        public class Response
        {
            public int GroupsCount { get; set; }
            public int CardsCount { get; set; }
            public int DailyRepeats { get; set; }
        }
    }
}