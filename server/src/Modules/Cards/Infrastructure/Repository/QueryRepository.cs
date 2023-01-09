using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain.ValueObjects;
using Cards.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Repository;

internal class QueryRepository : IQueryRepository
{
    private readonly CardsContext _cardsContext;

    public QueryRepository(CardsContext cardsContext)
    {
        _cardsContext = cardsContext;
    }

    public async Task<IEnumerable<Repeat>> GetRepeats(
        OwnerId ownerId,
        DateTime dateTime,
        int count,
        IEnumerable<int> questionLanguage,
        long groupId,
        bool lessonIncluded,
        CancellationToken cancellationToken)
    {
        return await _cardsContext.Repeats
            .Where(x =>
                x.OwnerId == ownerId.Value &&
                x.NextRepeat <= dateTime &&
                x.LessonIncluded == lessonIncluded &&
                (!questionLanguage.Any() || questionLanguage.Contains(x.QuestionLanguage)) &&
                (groupId == 0 || x.GroupId == groupId))
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetDailyRepeatsCount(OwnerId ownerId, DateTime dateTime, IEnumerable<int> questionLanguage, CancellationToken cancellationToken)
        => await _cardsContext.Repeats
            .CountAsync(x =>
                    x.OwnerId == ownerId.Value &&
                    x.NextRepeat <= dateTime &&
                    x.LessonIncluded == true &&
                    (!questionLanguage.Any() || questionLanguage.Contains(x.QuestionLanguage)),
                cancellationToken);

    public async Task<int> GetGroupsCount(OwnerId ownerId, CancellationToken cancellationToken)
        => await _cardsContext.Groups
            .CountAsync(x => x.OwnerId == ownerId, cancellationToken);

    public async Task<int> GetCardsCount(OwnerId ownerId, CancellationToken cancellationToken)
        => await _cardsContext.Details
            .CountAsync(x => x.OwnerId == ownerId, cancellationToken);

    public async Task<IEnumerable<RepeatCount>> GetRepeatsCountSummary(OwnerId userId, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken)
        => await _cardsContext.RepeatCounts.Where(x => x.OwnerId == userId.Value && x.Date >= dateFrom && x.Date <= dateTo).ToListAsync(cancellationToken);

    public async Task<int> GetNewRepeatsCount(OwnerId ownerId, int questionLanguage, long? groupId, CancellationToken cancellationToken)
        => await _cardsContext.Repeats
            .CountAsync(x =>
                    x.OwnerId == ownerId.Value &&
                    x.LessonIncluded == true &&
                    (!groupId.HasValue || x.GroupId == groupId) &&
                    (questionLanguage == 0 || x.QuestionLanguage == questionLanguage),
                cancellationToken);

    public async Task<IEnumerable<GroupSummary>> GetGroupSummaries(Guid ownerId, CancellationToken cancellationToken)
        => await _cardsContext.GroupSummaries.Where(x => x.OwnerId == ownerId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<CardSummary>> GetCardSummaries(Guid ownerId, long groupId, CancellationToken cancellationToken)
        => await _cardsContext.CardsDetails.Where(x => x.OwnerId == ownerId && x.GroupId == groupId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<CardSummary>> GetCardSummaries(long groupId, CancellationToken cancellationToken)
        => await _cardsContext.CardsDetails.Where(x => x.GroupId == groupId).ToListAsync(cancellationToken);

    public async Task<GroupSummary> GetGroupDetails(long groupId, CancellationToken cancellationToken)
        => await _cardsContext.GroupSummaries.SingleOrDefaultAsync(x => x.Id == groupId, cancellationToken);

    public async Task<IEnumerable<GroupToLesson>> GetGroups(Guid ownerId, CancellationToken cancellationToken)
        => await _cardsContext.GroupsToLesson
            .Where(x => x.OwnerId == ownerId && (x.FrontCount > 0 || x.BackCount > 0))
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<RepeatCount>> GetRepeatsPerDay(Guid ownerId, DateTime start, DateTime stop, CancellationToken cancellationToken)
        => await _cardsContext.RepeatCounts.Where(x => x.UserId == ownerId && x.Date >= start.Date && x.Date <= stop.Date).ToListAsync(cancellationToken);

    public async Task<IEnumerable<GroupSummary>> GetGroupSummaries(SearchGroupsQuery query, CancellationToken cancellationToken)
        => await _cardsContext.GroupSummaries
            .Where(x => x.Name.Contains(query.SearchingTerm))
            .Skip(query.Skip)
            .Take(query.Take)
            .ToListAsync(cancellationToken);

    public async Task<int> GetGroupSummariesCount(SearchGroupsQuery query, CancellationToken cancellationToken)
        => await _cardsContext.GroupSummaries
            .Where(x => x.Name.Contains(query.SearchingTerm))
            .CountAsync(cancellationToken);

    public async Task<IEnumerable<CardSummary>> SearchCards(SearchCardsQuery query, CancellationToken cancellationToken)
        => await _cardsContext.CardsDetails
            .Where(x => string.IsNullOrWhiteSpace(query.SearchingTerm) || x.FrontValue.Contains(query.SearchingTerm) || x.BackValue.Contains(query.SearchingTerm))
            .Where(x => !query.SearchingDrawers.Any() || query.SearchingDrawers.Contains(x.FrontDrawer) || query.SearchingDrawers.Contains(x.BackDrawer))
            .Where(x => !query.LessonIncluded.HasValue || x.BackLessonIncluded == query.LessonIncluded || x.FrontLessonIncluded == query.LessonIncluded)
            .Where(x => !query.OnlyTicked || x.BackIsTicked)
            .Where(x => x.OwnerId == query.OwnerId)
            .Skip(query.Skip)
            .Take(query.Take)
            .ToListAsync(cancellationToken);

    public async Task<int> SearchCardsCount(SearchCardsQuery query, CancellationToken cancellationToken)
        => await _cardsContext.CardsDetails
            .Where(x => string.IsNullOrWhiteSpace(query.SearchingTerm) || x.FrontValue.Contains(query.SearchingTerm) || x.BackValue.Contains(query.SearchingTerm))
            .Where(x => !query.SearchingDrawers.Any() || query.SearchingDrawers.Contains(x.FrontDrawer) || query.SearchingDrawers.Contains(x.BackDrawer))
            .Where(x => !query.LessonIncluded.HasValue || x.BackLessonIncluded == query.LessonIncluded || x.FrontLessonIncluded == query.LessonIncluded)
            .Where(x => !query.OnlyTicked || x.BackIsTicked)
            .Where(x => x.OwnerId == query.OwnerId)
            .CountAsync(cancellationToken);

    public async Task<CardsOverview> GetCardsOverview(Guid ownerId, CancellationToken cancellationToken)
        => await _cardsContext.CardsOverviews.FirstOrDefaultAsync(x => x.OwnerId == ownerId, cancellationToken);
}