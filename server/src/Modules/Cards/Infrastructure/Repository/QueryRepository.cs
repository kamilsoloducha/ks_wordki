using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries;
using Cards.Application.Services;
using Cards.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure
{
    internal class QueryRepository : IQueryRepository
    {
        private readonly CardsContext _context;

        public QueryRepository(CardsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Repeat>> GetRepeats2(UserId userId, DateTime dateTime, int count, int questionLanguage, CancellationToken cancellationToken)
        {
            return await _context.Repeats
                .Where(x =>
                    x.UserId == userId.Value &&
                    x.NextRepeat <= dateTime &&
                    (questionLanguage == 0 || x.QuestionLanguage == questionLanguage))
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetDailyRepeatsCount(UserId userId, DateTime dateTime, int questionLanguage, CancellationToken cancellationToken)
            => await _context.Repeats
                .CountAsync(x =>
                    x.UserId == userId.Value &&
                    x.NextRepeat <= dateTime &&
                    (questionLanguage == 0 || x.QuestionLanguage == questionLanguage),
                cancellationToken);

        public async Task<int> GetGroupsCount(UserId userId, CancellationToken cancellationToken)
            => await _context.Groups
                .CountAsync(x => x.CardsSet.UserId == userId, cancellationToken);

        public async Task<int> GetCardsCount(UserId userId, CancellationToken cancellationToken)
            => await _context.Sides
                .CountAsync(x => x.Card.Group.CardsSet.UserId == userId, cancellationToken);

        public async Task<IEnumerable<RepeatCount>> GetRepeatsCountSummary(UserId userId, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken)
            => await _context.RepeatCounts.Where(x => x.UserId == userId.Value && x.Date >= dateFrom && x.Date <= dateTo).ToListAsync(cancellationToken);
    }
}