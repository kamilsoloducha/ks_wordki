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
                if (!request.QuestionLanguage.HasValue)
                {
                    throw new Exception($"{nameof(request.QuestionLanguage)} is not defined");
                }
                // todo move validation to fluentValidation

                var userIdValue = _userDataProvider.GetUserId();
                var userId = UserId.Restore(userIdValue);
                var now = SystemClock.Now.Date;

                var repeats = await _queryRepository.GetDailyRepeatsCount(userId, now, request.QuestionLanguage.Value, cancellationToken);

                return repeats;
            }
        }

        public class Query : IRequest<int>
        {
            public int? QuestionLanguage { get; set; }
        }
    }
}