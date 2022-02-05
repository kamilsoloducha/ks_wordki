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
    public class GetRepeats
    {
        internal class QueryHandler : IRequestHandler<Query, Response>
        {
            private readonly IQueryRepository _queryRepository;

            public QueryHandler(IQueryRepository queryRepository)
            {
                _queryRepository = queryRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!request.Count.HasValue) throw new Exception($"{nameof(request.Count)} must be defined");
                if (!request.QuestionLanguage.HasValue) throw new Exception($"{nameof(request.QuestionLanguage)} must be defined");

                var ownerId = OwnerId.Restore(request.OwnerId);
                var now = request.LessonIncluded ? SystemClock.Now.Date : DateTime.MaxValue;
                var groupId = request.GroupId.HasValue ? request.GroupId.Value : 0;
                var repeats = await _queryRepository.GetRepeats(
                    ownerId,
                    now,
                    request.Count.Value,
                    request.QuestionLanguage.Value,
                    groupId,
                    request.LessonIncluded,
                    cancellationToken
                    );

                return new Response
                {
                    Repeats = repeats,
                };
            }
        }

        public class Query : IRequest<Response>
        {
            public Guid OwnerId { get; set; }
            public long? GroupId { get; set; }
            public int? Count { get; set; }
            public int? QuestionLanguage { get; set; }
            public bool LessonIncluded { get; set; } = true;
        }

        public class Response
        {
            public IEnumerable<Repeat> Repeats { get; set; }
        }

    }
}