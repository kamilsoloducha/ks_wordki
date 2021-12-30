using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Services;
using Cards.Application.Services;
using Cards.Domain;
using MediatR;
using Utils;

namespace Cards.Application.Queries
{
    public class GetRepeats
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
                if (!request.Count.HasValue) throw new Exception($"{nameof(request.Count)} must be defined");
                if (!request.QuestionLanguage.HasValue) throw new Exception($"{nameof(request.QuestionLanguage)} must be defined");

                var userIdValue = _userDataProvider.GetUserId();
                var userId = UserId.Restore(userIdValue);
                var now = SystemClock.Now.Date;
                var repeats = await _queryRepository.GetRepeats(userId, now, request.Count.Value, request.QuestionLanguage.Value, cancellationToken);

                return new Response
                {
                    Repeats = repeats,
                };
            }
        }

        public class Query : IRequest<Response>
        {
            public int? Count { get; set; }
            public int? QuestionLanguage { get; set; }
        }

        public class Response
        {
            public IEnumerable<Repeat> Repeats { get; set; }
        }

    }
}