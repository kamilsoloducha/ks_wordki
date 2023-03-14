using System.Threading;
using System.Threading.Tasks;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Cards.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Repository
{
    internal class CardsRepository : IOwnerRepository
    {
        private readonly CardsContext _cardsContext;

        public CardsRepository(CardsContext cardsContext)
        {
            _cardsContext = cardsContext;
        }

        public Task<Owner> Get(UserId id, CancellationToken cancellationToken)
            => _cardsContext.Owners
                .Include(x => x.Groups)
                .ThenInclude(x => x.Cards)
                .FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);

        public Task<Group> GetGroup(UserId userId, long id, CancellationToken cancellationToken)
            => _cardsContext.Groups.FirstOrDefaultAsync(x => x.Id == id && x.Owner.UserId == userId, cancellationToken);

        public Task<Group> GetGroup(long id, CancellationToken cancellationToken)
            => _cardsContext.Groups.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public Task<Card> GetCard(UserId userId, long id, CancellationToken cancellationToken)
            => _cardsContext.Cards.SingleOrDefaultAsync(x => x.Id == id && x.Group.Owner.UserId == userId,
                cancellationToken);

        public Task Update(CancellationToken cancellationToken)
            => _cardsContext.SaveChangesAsync(cancellationToken);

        public Task Add(Owner owner, CancellationToken cancellationToken)
        {
            _cardsContext.Owners.Attach(owner);
            return _cardsContext.SaveChangesAsync(cancellationToken);
        }
    }
}