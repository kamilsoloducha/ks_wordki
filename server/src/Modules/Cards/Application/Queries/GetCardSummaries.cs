using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain;
using Cards.Domain.Enums;
using MediatR;

namespace Cards.Application.Queries;

public class GetCardSummaries
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
            var groupId = _hash.GetLongId(request.GroupId);
            var cards = await _queryRepository.GetCardSummaries(request.OwnerId, groupId, cancellationToken);
            return new Response
            {
                Cards = cards.Select(x => ToDto(x, _hash))
            };
        }

        private CardSummaryDto ToDto(CardSummary cardSummary, IHashIdsService _hash)
        {
            return new CardSummaryDto
            {
                Id = _hash.GetHash(cardSummary.CardId),
                Front = new SideSummaryDto
                {
                    Type = (int)SideType.Front,
                    Value = cardSummary.FrontValue,
                    Example = cardSummary.FrontExample,
                    Comment = cardSummary.FrontDetailsComment,
                    Drawer = Math.Min(cardSummary.FrontDrawer + 1, 5),
                    IsUsed = cardSummary.FrontLessonIncluded,
                    IsTicked = cardSummary.FrontIsTicked,
                },
                Back = new SideSummaryDto
                {
                    Type = (int)SideType.Back,
                    Value = cardSummary.BackValue,
                    Example = cardSummary.BackExample,
                    Comment = cardSummary.BackDetailsComment,
                    Drawer = Math.Min(cardSummary.BackDrawer + 1, 5),
                    IsUsed = cardSummary.BackLessonIncluded,
                    IsTicked = cardSummary.BackIsTicked,
                },
            };
        }
    }

    public class Query : IRequest<Response>
    {
        public Guid OwnerId { get; set; }
        public string GroupId { get; set; }
    }

    public class Response
    {
        public IEnumerable<CardSummaryDto> Cards { get; set; }
    }

    public class CardSummaryDto
    {
        public string Id { get; set; }
        public SideSummaryDto Back { get; set; }
        public SideSummaryDto Front { get; set; }
    }

    public class SideSummaryDto
    {
        public int Type { get; set; }
        public string Value { get; set; }
        public string Example { get; set; }
        public string Comment { get; set; }
        public int Drawer { get; set; }
        public bool IsUsed { get; set; }
        public bool IsTicked { get; set; }
    }
}