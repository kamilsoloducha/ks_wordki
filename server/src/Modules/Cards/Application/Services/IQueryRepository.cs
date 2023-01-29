using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries;
using Cards.Application.Queries.Models;
using Cards.Domain;
using Cards.Domain.ValueObjects;

namespace Cards.Application.Services;

public interface IQueryRepository
{
    Task<IEnumerable<Repeat>> GetRepeats(OwnerId ownerId,
        DateTime dateTime,
        int count,
        IEnumerable<int> questionLanguage,
        long groupId,
        bool lessonIncluded,
        CancellationToken cancellationToken);
    Task<int> GetDailyRepeatsCount(OwnerId ownerId, DateTime dateTime, IEnumerable<int> questionLanguage, CancellationToken cancellationToken);
    Task<int> GetNewRepeatsCount(OwnerId ownerId, int questionLanguage, long? groupId, CancellationToken cancellationToken);
    Task<int> GetGroupsCount(OwnerId ownerId, CancellationToken cancellationToken);
    Task<int> GetCardsCount(OwnerId ownerId, CancellationToken cancellationToken);
    Task<IEnumerable<RepeatCount>> GetRepeatsCountSummary(OwnerId ownerId, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken);


    Task<IEnumerable<GroupSummary>> GetGroupSummaries(Guid ownerId, CancellationToken cancellationToken);
    Task<IEnumerable<GroupSummary>> GetGroupSummaries(SearchGroupsQuery query, CancellationToken cancellationToken);
    Task<int> GetGroupSummariesCount(SearchGroupsQuery query, CancellationToken cancellationToken);
    Task<CardSummary> GetCardSummary(Guid ownerId, long groupId, long cardId, CancellationToken cancellationToken);
    Task<IEnumerable<CardSummary>> GetCardSummaries(Guid ownerId, long groupId, CancellationToken cancellationToken);
    Task<IEnumerable<CardSummary>> GetCardSummaries(long groupId, CancellationToken cancellationToken);
    Task<GroupSummary> GetGroupDetails(long groupId, CancellationToken cancellationToken);
    Task<IEnumerable<GroupToLesson>> GetGroups(Guid ownerId, CancellationToken cancellationToken);
    Task<IEnumerable<RepeatCount>> GetRepeatsPerDay(Guid ownerId, DateTime start, DateTime stop, CancellationToken cancellationToken);
    Task<IEnumerable<CardSummary>> SearchCards(SearchCardsQuery query, CancellationToken cancellationToken);
    Task<int> SearchCardsCount(SearchCardsQuery query, CancellationToken cancellationToken);
    Task<CardsOverview> GetCardsOverview(Guid owerId, CancellationToken cancellationToken);
}