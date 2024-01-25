using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;

namespace Cards.Application.Abstraction;

public interface IQueryRepository
{
    Task<IReadOnlyList<Repeat>> GetRepeats(UserId userId,
        DateTime dateTime,
        int? count,
        IEnumerable<string> questionLanguage,
        long? groupId,
        bool lessonIncluded,
        CancellationToken cancellationToken);

    Task<int> GetDailyRepeatsCount(UserId userId, DateTime dateTime, string[] languages,
        CancellationToken cancellationToken);

    Task<int> GetNewRepeatsCount(UserId userId, string questionLanguage, long? groupId,
        CancellationToken cancellationToken);

    Task<int> GetGroupsCount(UserId userId, CancellationToken cancellationToken);
    Task<int> GetCardsCount(UserId userId, CancellationToken cancellationToken);

    Task<IReadOnlyList<RepeatCount>> GetRepeatsCountSummary(UserId userId, DateTime dateFrom, DateTime dateTo,
        CancellationToken cancellationToken);


    Task<IReadOnlyList<GroupSummary>> GetGroupSummaries(Guid ownerId, CancellationToken cancellationToken);
    Task<IReadOnlyList<GroupSummary>> GetGroupSummaries(SearchGroupsQuery query, CancellationToken cancellationToken);
    Task<int> GetGroupSummariesCount(SearchGroupsQuery query, CancellationToken cancellationToken);
    Task<IReadOnlyList<Card>> GetCards(UserId ownerId, long groupId, CancellationToken cancellationToken);
    Task<Card> GetCard(UserId userId, long cardId, CancellationToken cancellationToken);
    Task<IReadOnlyList<CardSummary>> GetCardSummaries(long groupId, CancellationToken cancellationToken);
    Task<Group> GetGroup(Guid userId, long groupId, CancellationToken cancellationToken);
    Task<IReadOnlyList<GroupToLesson>> GetGroups(Guid ownerId, CancellationToken cancellationToken);

    Task<IReadOnlyList<RepeatCount>> GetRepeatsPerDay(Guid ownerId, DateTime start, DateTime stop,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<CardSummary>> SearchCards(SearchCardsQuery query, CancellationToken cancellationToken);
    Task<int> SearchCardsCount(SearchCardsQuery query, CancellationToken cancellationToken);
    Task<CardsOverview> GetCardsOverview(Guid owerId, CancellationToken cancellationToken);
    IEnumerable<LanguageDto> GetLanguages(UserId userId, CancellationToken cancellationToken);
}