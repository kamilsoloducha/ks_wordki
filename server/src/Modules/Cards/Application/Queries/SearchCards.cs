using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Services;
using Cards.Domain.Enums;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Queries;

public class SearchCards
{
    internal class QueryHandler : IRequestHandler<Query, IEnumerable<CardSummary>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }

        public async Task<IEnumerable<CardSummary>> Handle(Query request, CancellationToken cancellationToken)
        {
            var searchQuery = SearchCardsQuery.Create(
                request.OwnerId,
                request.SearchingTerm,
                request.SearchingDrawers,
                request.LessonIncluded,
                request.OnlyTicked,
                request.PageNumber,
                request.PageSize);

            var cards = await _queryRepository.SearchCards(searchQuery, cancellationToken);
            return cards.Select(ToDto);
        }

        private CardSummary ToDto(Models.CardSummary card)
        {
            return new CardSummary
            {
                Id = _hash.GetHash(card.CardId),
                GroupId = _hash.GetHash(card.GroupId),
                GroupName = card.GroupName,
                Front = new SideSummary
                {
                    Type = (int)SideType.Front,
                    Lang = card.FrontLanguage,
                    Value = card.FrontValue,
                    Example = card.FrontExample,
                    Comment = card.FrontDetailsComment,
                    Drawer = new Drawer(card.FrontDrawer).Value,
                    IsUsed = card.FrontLessonIncluded,
                    IsTicked = card.FrontIsTicked,
                },
                Back = new SideSummary
                {
                    Type = (int)SideType.Back,
                    Lang = card.BackLanguage,
                    Value = card.BackValue,
                    Example = card.BackExample,
                    Comment = card.BackDetailsComment,
                    Drawer = new Drawer(card.BackDrawer).Value,
                    IsUsed = card.BackLessonIncluded,
                    IsTicked = card.BackIsTicked,
                },
            };
        }
    }

    public class Query : IRequest<IEnumerable<CardSummary>>
    {
        public Guid OwnerId { get; set; }
        public string SearchingTerm { get; set; }
        public IEnumerable<int> SearchingDrawers { get; set; }
        public bool? LessonIncluded { get; set; }
        public bool OnlyTicked { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class CardSummary
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public SideSummary Back { get; set; }
        public SideSummary Front { get; set; }
    }

    public class SideSummary
    {
        public int Type { get; set; }
        public int Lang { get; set; }
        public string Value { get; set; }
        public string Example { get; set; }
        public string Comment { get; set; }
        public int Drawer { get; set; }
        public bool IsUsed { get; set; }
        public bool IsTicked { get; set; }
    }


}