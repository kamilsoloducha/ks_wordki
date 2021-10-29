using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Services;
using Cards.Application.Services;
using Cards.Domain;
using MediatR;

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
                var userIdValue = _userDataProvider.GetUserId();
                var userId = UserId.Restore(userIdValue);
                var repeats = await _queryRepository.GetRepeats2(userId, request.Count, cancellationToken);

                return new Response
                {
                    Repeats = repeats,
                };
            }
        }

        public class Query : IRequest<Response>
        {
            public int Count { get; set; }
        }

        public class Response
        {
            public IEnumerable<Repeat> Repeats { get; set; }
        }

    }
}