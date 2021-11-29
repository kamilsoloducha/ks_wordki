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
    public class GetRepeatsCount
    {
        internal class QueryHandler : IRequestHandler<Query, int>
        {
            private readonly IQueryRepository _queryRepository;
            private readonly IUserDataProvider _userDataProvider;

            public QueryHandler(IQueryRepository queryRepository,
                IUserDataProvider userDataProvider)
            {
                _queryRepository = queryRepository;
                _userDataProvider = userDataProvider;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                var userIdValue = _userDataProvider.GetUserId();
                var userId = UserId.Restore(userIdValue);
                var now = SystemClock.Now.Date;

                var repeats = await _queryRepository.GetDailyRepeatsCount(userId, now, cancellationToken);

                return repeats;
            }
        }

        public class Query : IRequest<int> { }
    }
}