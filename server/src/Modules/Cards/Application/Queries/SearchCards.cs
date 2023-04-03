using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Services;
using Cards.Domain.Enums;
using Cards.Domain.OwnerAggregate;
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
                request.UserId,
                request.SearchingTerm,
                request.SearchingDrawers,
                request.LessonIncluded,
                request.Ticked,
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
                    Lang = card.Front,
                    Value = card.FrontValue,
                    Example = card.FrontExample,
                    Comment = string.Empty,
                    Drawer = Drawer.ToValue(card.FrontDrawer),
                    IsUsed = card.FrontIsQuestion,
                    IsTicked = card.FrontIsTicked,
                },
                Back = new SideSummary
                {
                    Type = (int)SideType.Back,
                    Lang = card.Back,
                    Value = card.BackValue,
                    Example = card.BackExample,
                    Comment = string.Empty,
                    Drawer = Drawer.ToValue(card.BackDrawer),
                    IsUsed = card.BackIsQuestion,
                    IsTicked = card.BackIsTicked,
                },
            };
        }
    }

    public record Query(
        Guid UserId,
        string SearchingTerm,
        IEnumerable<int> SearchingDrawers,
        bool? LessonIncluded,
        bool? Ticked,
        int PageNumber,
        int PageSize) : IRequest<IEnumerable<CardSummary>>;

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
        public string Lang { get; set; }
        public string Value { get; set; }
        public string Example { get; set; }
        public string Comment { get; set; }
        public int Drawer { get; set; }
        public bool IsUsed { get; set; }
        public bool IsTicked { get; set; }
    }
}