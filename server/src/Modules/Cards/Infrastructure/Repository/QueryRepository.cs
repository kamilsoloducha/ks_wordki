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

        public async Task<List<CardSide>> GetRepeats(UserId userId, int count, CancellationToken cancellationToken)
        {
            return await _context.Sides
                .Include(s => s.Card)
                .Where(x => x.Card.Group.CardsSet.UserId == userId)
                .OrderBy(x => x.NextRepeat)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Repeat>> GetRepeats2(UserId userId, int count, CancellationToken cancellationToken)
        {
            return await _context.Repeats
                .Where(x => x.UserId == userId.Value)
                .Take(count)
                .ToListAsync(cancellationToken);
        }
    }
}