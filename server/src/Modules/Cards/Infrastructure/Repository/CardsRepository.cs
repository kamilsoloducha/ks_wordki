using System.Threading;
using System.Threading.Tasks;
using Cards.Domain2;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure2
{
    internal class CardsRepository : ICardsRepository
    {
        private readonly CardsContextNew _cardsContext;

        public CardsRepository(CardsContextNew cardsContext)
        {
            _cardsContext = cardsContext;
        }

        public async Task Add(Owner owner, CancellationToken cancellationToken)
        {
            await _cardsContext.Owners.AddAsync(owner, cancellationToken);
            await _cardsContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Owner> Get(OwnerId id, CancellationToken cancellationToken)
            => await _cardsContext.Owners
                        .Include(x => x.Groups)
                            .ThenInclude(x => x.Cards)
                            .ThenInclude(x => x.Front).AsSplitQuery()
                        .Include(x => x.Groups)
                            .ThenInclude(x => x.Cards)
                            .ThenInclude(x => x.Back).AsSplitQuery()
                        .Include(x => x.Details).AsSplitQuery()
                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<Detail> Get(OwnerId ownerId, SideId sideId, CancellationToken cancellationToken)
            => await _cardsContext.Details
                .FirstOrDefaultAsync(x => x.OwnerId == ownerId && x.SideId == sideId, cancellationToken);

        public async Task Update(Owner owner, CancellationToken cancellationToken)
        {
            _cardsContext.Owners.Update(owner);
            await _cardsContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(CancellationToken cancellationToken)
        {
            await _cardsContext.SaveChangesAsync(cancellationToken);
        }
    }
}
