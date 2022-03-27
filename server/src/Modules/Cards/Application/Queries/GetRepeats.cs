using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
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
            private readonly IHashIdsService _hash;

            public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
            {
                _queryRepository = queryRepository;
                _hash = hash;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!request.Count.HasValue) throw new Exception($"{nameof(request.Count)} must be defined");

                var groupId = string.IsNullOrEmpty(request.GroupId) ? 0 : _hash.GetLongId(request.GroupId);

                var ownerId = OwnerId.Restore(request.OwnerId);
                var now = request.LessonIncluded ? SystemClock.Now.Date : DateTime.MaxValue;
                var repeats = await _queryRepository.GetRepeats(
                    ownerId,
                    now,
                    request.Count.Value,
                    request.QuestionLanguage,
                    groupId,
                    request.LessonIncluded,
                    cancellationToken
                    );

                return new Response
                {
                    Repeats = repeats.Select(x => ToDto(x, _hash)),
                };
            }

            private RepeatDto ToDto(Repeat repeat, IHashIdsService hash)
                => new RepeatDto
                {
                    GroupId = _hash.GetHash(repeat.GroupId),
                    CardId = _hash.GetHash(repeat.CardId),
                    SideId = _hash.GetHash(repeat.SideId),
                    QuestionDrawer = repeat.QuestionDrawer,
                    Question = repeat.Question,
                    QuestionExample = repeat.QuestionExample,
                    QuestionType = repeat.QuestionType,
                    QuestionLanguage = repeat.QuestionLanguage,
                    Answer = repeat.Answer,
                    AnswerExample = repeat.AnswerExample,
                    AnswerType = repeat.AnswerType,
                    AnswerLanguage = repeat.AnswerLanguage,
                    Comment = repeat.Comment,
                    GroupName = repeat.GroupName,
                    FrontLanguage = repeat.FrontLanguage,
                    BackLanguage = repeat.BackLanguage,
                };
        }

        public class Query : IRequest<Response>
        {
            public Guid OwnerId { get; set; }
            public string GroupId { get; set; }
            public int? Count { get; set; }
            public IEnumerable<int> QuestionLanguage { get; set; }
            public bool LessonIncluded { get; set; } = true;
        }

        public class Response
        {
            public IEnumerable<RepeatDto> Repeats { get; set; }
        }

        public class RepeatDto
        {
            public string GroupId { get; set; }
            public string CardId { get; set; }
            public string SideId { get; set; }
            public int QuestionDrawer { get; set; }
            public string Question { get; set; }
            public string QuestionExample { get; set; }
            public int QuestionType { get; set; }
            public int QuestionLanguage { get; set; }
            public string Answer { get; set; }
            public string AnswerExample { get; set; }
            public int AnswerType { get; set; }
            public int AnswerLanguage { get; set; }
            public string Comment { get; set; }
            public string GroupName { get; set; }
            public int FrontLanguage { get; set; }
            public int BackLanguage { get; set; }
        }

    }
}