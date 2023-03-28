using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using Domain.Utils;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetRepeats
{
    internal sealed class QueryHandler : IRequestHandler<Query, IEnumerable<RepeatDto>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }

        public async Task<IEnumerable<RepeatDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var ownerId = UserId.Restore(request.OwnerId);

            var repeats = await _queryRepository.GetRepeats(
                ownerId,
                RepeatPeriod.To,
                request.Count,
                request.Languages,
                request.GroupId,
                request.LessonIncluded,
                cancellationToken
            );
            return repeats.Select(ToDto);
        }

        private RepeatDto ToDto(Repeat repeat)
            => new(CardId: _hash.GetHash(repeat.CardId), SideType: repeat.SideType,
                QuestionDrawer: repeat.QuestionDrawer, Question: repeat.Question,
                QuestionExample: repeat.QuestionExample, QuestionLanguage: repeat.QuestionLanguage,
                Answer: repeat.Answer, AnswerExample: repeat.AnswerExample, AnswerLanguage: repeat.AnswerLanguage);
    }

    public record Query(
            Guid OwnerId,
            long? GroupId,
            int? Count,
            IEnumerable<string> Languages,
            bool LessonIncluded)
        : IRequest<IEnumerable<RepeatDto>>;

    
}