using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Cards.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Repository
{
    internal class QueryRepository : IQueryRepository
    {
        private readonly CardsContext _cardsContext;

        public QueryRepository(CardsContext cardsContext)
        {
            _cardsContext = cardsContext;
        }

        public async Task<IEnumerable<Repeat>> GetRepeats(
            UserId userId,
            DateTime dateTime,
            int? count,
            IEnumerable<string> questionLanguage,
            long? groupId,
            bool lessonIncluded,
            CancellationToken cancellationToken)
        {
            var languages = questionLanguage.ToArray();
            var realCount = count ?? int.MaxValue;

            return lessonIncluded
                ? groupId.HasValue
                    ? await _cardsContext.Repeats
                        .Where(x =>
                            x.UserId == userId.Value &&
                            x.LessonIncluded == lessonIncluded &&
                            (!languages.Any() || languages.Contains(x.QuestionLanguage)) &&
                            x.NextRepeat <= dateTime &&
                            x.GroupId == groupId)
                        .Take(realCount)
                        .ToListAsync(cancellationToken)
                    : await _cardsContext.Repeats
                        .Where(x =>
                            x.UserId == userId.Value &&
                            x.LessonIncluded == lessonIncluded &&
                            (!languages.Any() || languages.Contains(x.QuestionLanguage)) &&
                            x.NextRepeat <= dateTime)
                        .Take(realCount)
                        .ToListAsync(cancellationToken)
                : await _cardsContext.Repeats
                    .Where(x =>
                        x.UserId == userId.Value &&
                        x.LessonIncluded == lessonIncluded &&
                        (!languages.Any() || languages.Contains(x.QuestionLanguage)) &&
                        x.NextRepeat == null &&
                        groupId.HasValue && x.GroupId == groupId)
                    .Take(realCount)
                    .ToListAsync(cancellationToken);
        }

        // public Task<int> GetDailyRepeatsCount(UserId userId, DateTime dateTime, IEnumerable<int> questionLanguage, CancellationToken cancellationToken)
        //     => _cardsContext.Repeats
        //         .CountAsync(x =>
        //                 x.OwnerId == userId.Value &&
        //                 x.NextRepeat <= dateTime &&
        //                 x.LessonIncluded == true &&
        //                 (!questionLanguage.Any() || questionLanguage.Contains(x.QuestionLanguage)),
        //             cancellationToken);

        public Task<int> GetDailyRepeatsCount(UserId userId, DateTime dateTime, IEnumerable<string> questionLanguage,
            CancellationToken cancellationToken)
            => _cardsContext.Details.CountAsync(
                x => x.Card.Group.Owner.UserId == userId &&
                     x.IsQuestion == true &&
                     x.NextRepeat <= dateTime, cancellationToken
            );

        public Task<int> GetGroupsCount(UserId userId, CancellationToken cancellationToken)
            => _cardsContext.Groups
                .CountAsync(x => x.Owner.UserId == userId, cancellationToken);

        public Task<int> GetCardsCount(UserId userId, CancellationToken cancellationToken)
            => _cardsContext.Details
                .CountAsync(x => x.Card.Group.Owner.UserId == userId, cancellationToken);

        public async Task<IEnumerable<RepeatCount>> GetRepeatsCountSummary(UserId userId, DateTime dateFrom,
            DateTime dateTo, CancellationToken cancellationToken)
            => await _cardsContext.RepeatCounts
                .Where(x => x.OwnerId == userId.Value && x.Date >= dateFrom && x.Date <= dateTo)
                .ToListAsync(cancellationToken);

        public Task<int> GetNewRepeatsCount(UserId userId, string questionLanguage, long? groupId,
            CancellationToken cancellationToken)
            => _cardsContext.Repeats
                .CountAsync(x =>
                        x.UserId == userId.Value &&
                        x.LessonIncluded == true &&
                        (!groupId.HasValue || x.GroupId == groupId) &&
                        (questionLanguage == string.Empty || x.QuestionLanguage == questionLanguage),
                    cancellationToken);

        public async Task<IEnumerable<GroupSummary>> GetGroupSummaries(Guid ownerId,
            CancellationToken cancellationToken)
            => await _cardsContext.Groups
                .Where(x => x.Owner.UserId == new UserId(ownerId))
                .Select(x => new GroupSummary(x.Id, x.Name.Text, x.Front, x.Back, x.Cards.Count))
                .ToListAsync(cancellationToken);

        public Task<CardSummary> GetCardSummary(Guid ownerId, long groupId, long cardId,
            CancellationToken cancellationToken)
            => _cardsContext.CardsDetails.SingleOrDefaultAsync(
                x => x.OwnerId == ownerId && x.GroupId == groupId && x.CardId == cardId, cancellationToken);

        public async Task<IEnumerable<Card>> GetCards(UserId ownerId, long groupId, CancellationToken cancellationToken)
            => await _cardsContext.Cards
                .Where(x => x.Group.Owner.UserId == ownerId && x.Group.Id == groupId)
                .ToListAsync(cancellationToken);

        public Task<Card> GetCard(UserId ownerId, long cardId, CancellationToken cancellationToken)
            => _cardsContext.Cards.SingleOrDefaultAsync(x => x.Group.Owner.UserId == ownerId && x.Id == cardId,
                cancellationToken);

        public async Task<IEnumerable<CardSummary>> GetCardSummaries(long groupId, CancellationToken cancellationToken)
            => await _cardsContext.CardsDetails.Where(x => x.GroupId == groupId).ToListAsync(cancellationToken);

        public Task<Group> GetGroup(Guid userId, long groupId, CancellationToken cancellationToken)
            => _cardsContext.Groups
                .Include(x => x.Cards)
                .SingleOrDefaultAsync(x => x.Id == groupId, cancellationToken);


        public async Task<IEnumerable<GroupToLesson>> GetGroups(Guid ownerId, CancellationToken cancellationToken)
            => await _cardsContext.GroupsToLesson
                .Where(x => x.OwnerId == ownerId && (x.FrontCount > 0 || x.BackCount > 0))
                .ToListAsync(cancellationToken);

        public async Task<IEnumerable<RepeatCount>> GetRepeatsPerDay(Guid ownerId, DateTime start, DateTime stop,
            CancellationToken cancellationToken)
            => await _cardsContext.RepeatCounts
                .Where(x => x.OwnerId == ownerId && x.Date >= start.Date && x.Date <= stop.Date)
                .ToListAsync(cancellationToken);


        public async Task<IEnumerable<GroupSummary>> GetGroupSummaries(SearchGroupsQuery query,
            CancellationToken cancellationToken)
            => await _cardsContext.GroupSummaries
                .Where(x => x.Name.Contains(query.SearchingTerm))
                .Skip(query.Skip)
                .Take(query.Take)
                .ToListAsync(cancellationToken);

        public Task<int> GetGroupSummariesCount(SearchGroupsQuery query, CancellationToken cancellationToken)
            => _cardsContext.GroupSummaries
                .Where(x => x.Name.Contains(query.SearchingTerm))
                .CountAsync(cancellationToken);

        public async Task<IEnumerable<CardSummary>> SearchCards(SearchCardsQuery query,
            CancellationToken cancellationToken)
            => await _cardsContext.CardsDetails
                .Where(x => string.IsNullOrWhiteSpace(query.SearchingTerm) ||
                            x.FrontValue.Contains(query.SearchingTerm) ||
                            x.BackValue.Contains(query.SearchingTerm))
                .Where(x => !query.SearchingDrawers.Any() || query.SearchingDrawers.Contains(x.FrontDrawer) ||
                            query.SearchingDrawers.Contains(x.BackDrawer))
                .Where(x => !query.LessonIncluded.HasValue || x.BackLessonIncluded == query.LessonIncluded ||
                            x.FrontLessonIncluded == query.LessonIncluded)
                .Where(x => !query.OnlyTicked || x.BackIsTicked)
                .Where(x => x.OwnerId == query.OwnerId)
                .Skip(query.Skip)
                .Take(query.Take)
                .ToListAsync(cancellationToken);

        public Task<int> SearchCardsCount(SearchCardsQuery query, CancellationToken cancellationToken)
            => _cardsContext.CardsDetails
                .Where(x => string.IsNullOrWhiteSpace(query.SearchingTerm) ||
                            x.FrontValue.Contains(query.SearchingTerm) ||
                            x.BackValue.Contains(query.SearchingTerm))
                .Where(x => !query.SearchingDrawers.Any() || query.SearchingDrawers.Contains(x.FrontDrawer) ||
                            query.SearchingDrawers.Contains(x.BackDrawer))
                .Where(x => !query.LessonIncluded.HasValue || x.BackLessonIncluded == query.LessonIncluded ||
                            x.FrontLessonIncluded == query.LessonIncluded)
                .Where(x => !query.OnlyTicked || x.BackIsTicked)
                .Where(x => x.OwnerId == query.OwnerId)
                .CountAsync(cancellationToken);

        public Task<CardsOverview> GetCardsOverview(Guid ownerId, CancellationToken cancellationToken)
            => _cardsContext.CardsOverviews.FirstOrDefaultAsync(x => x.OwnerId == ownerId, cancellationToken);

        public Task<IEnumerable<LanguageDto>> GetLanguages(UserId userId, CancellationToken cancellationToken)
            => Task.FromResult(_cardsContext.Groups.Where(x => x.Owner.UserId == userId).Select(x => x.Front)
                .Union(_cardsContext.Groups.Where(x => x.Owner.UserId == userId).Select(x => x.Back))
                .Select(x => new LanguageDto(x)).AsEnumerable());
    }
}