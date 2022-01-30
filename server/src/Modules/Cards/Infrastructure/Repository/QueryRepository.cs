using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain2;
using Cards.Infrastructure2;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure
{
    internal class QueryRepository : IQueryRepository
    {
        private readonly CardsContextNew _cardsContext;

        public QueryRepository(CardsContextNew cardsContext)
        {
            _cardsContext = cardsContext;
        }

        public async Task<IEnumerable<Repeat>> GetRepeats(OwnerId ownerId, DateTime dateTime, int count, int questionLanguage, CancellationToken cancellationToken)
        {
            return await _cardsContext.Repeats
                .Where(x =>
                    x.OwnerId == ownerId.Value &&
                    x.NextRepeat <= dateTime &&
                    (questionLanguage == 0 || x.QuestionLanguage == questionLanguage))
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetDailyRepeatsCount(OwnerId ownerId, DateTime dateTime, int questionLanguage, CancellationToken cancellationToken)
            => await _cardsContext.Repeats
                .CountAsync(x =>
                    x.OwnerId == ownerId.Value &&
                    x.NextRepeat <= dateTime &&
                    (questionLanguage == 0 || x.QuestionLanguage == questionLanguage),
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
                    (!groupId.HasValue || x.GroupId == groupId) &&
                    (questionLanguage == 0 || x.QuestionLanguage == questionLanguage),
                cancellationToken);

        public async Task<IEnumerable<GroupSummary>> GetGroupSummaries(Guid ownerId, CancellationToken cancellationToken)
            => await _cardsContext.GroupSummaries.Where(x => x.OwnerId == ownerId).ToListAsync(cancellationToken);

        public async Task<IEnumerable<CardSummary>> GetCardSummaries(Guid ownerId, long groupId, CancellationToken cancellationToken)
            => await _cardsContext.CardsDetails.Where(x => x.OwnerId == ownerId && x.GroupId == groupId).ToListAsync(cancellationToken);

        public async Task<GroupSummary> GetGroupDetails(long groupId, CancellationToken cancellationToken)
            => await _cardsContext.GroupSummaries.SingleOrDefaultAsync(x => x.Id == groupId, cancellationToken);
    }
}