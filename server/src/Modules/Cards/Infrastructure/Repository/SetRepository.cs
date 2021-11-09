using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure
{
    internal class SetRepository : ISetRepository
    {
        private readonly CardsContext _context;

        public SetRepository(CardsContext context)
        {
            _context = context;
        }

        public async Task Add(Set cardsSet, CancellationToken cancellationToken)
        {
            await _context.CardsSet.AddAsync(cardsSet, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Set cardsSet, CancellationToken cancellationToken)
        {
            var newGroups = cardsSet.Groups.Where(x => x.IsNew);
            await _context.Groups.AddRangeAsync(newGroups, cancellationToken);

            var newCards = cardsSet.Groups.Where(x => x.IsDirty).SelectMany(x => x.Cards).Where(x => x.IsNew);
            await _context.Cards.AddRangeAsync(newCards, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Set> Get(UserId userId, CancellationToken cancellationToken)
            => await _context.CardsSet
            .Include(s => s.Groups)
            .ThenInclude(g => g.Cards)
            .ThenInclude(c => c.Sides)
            .SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken);


    }
}