using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;

namespace Cards.Application.Services;

public interface IQueryRepository
{
    Task<IEnumerable<Repeat>> GetRepeats(UserId userId,
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

    Task<IEnumerable<RepeatCount>> GetRepeatsCountSummary(UserId userId, DateTime dateFrom, DateTime dateTo,
        CancellationToken cancellationToken);


    Task<IEnumerable<GroupSummary>> GetGroupSummaries(Guid ownerId, CancellationToken cancellationToken);
    Task<IEnumerable<GroupSummary>> GetGroupSummaries(SearchGroupsQuery query, CancellationToken cancellationToken);
    Task<int> GetGroupSummariesCount(SearchGroupsQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Card>> GetCards(UserId ownerId, long groupId, CancellationToken cancellationToken);
    Task<Card> GetCard(UserId userId, long cardId, CancellationToken cancellationToken);
    Task<IEnumerable<CardSummary>> GetCardSummaries(long groupId, CancellationToken cancellationToken);
    Task<Group> GetGroup(Guid userId, long groupId, CancellationToken cancellationToken);
    Task<IEnumerable<GroupToLesson>> GetGroups(Guid ownerId, CancellationToken cancellationToken);

    Task<IEnumerable<RepeatCount>> GetRepeatsPerDay(Guid ownerId, DateTime start, DateTime stop,
        CancellationToken cancellationToken);

    Task<IEnumerable<CardSummary>> SearchCards(SearchCardsQuery query, CancellationToken cancellationToken);
    Task<int> SearchCardsCount(SearchCardsQuery query, CancellationToken cancellationToken);
    Task<CardsOverview> GetCardsOverview(Guid owerId, CancellationToken cancellationToken);
    Task<IEnumerable<LanguageDto>> GetLanguages(UserId userId, CancellationToken cancellationToken);

}