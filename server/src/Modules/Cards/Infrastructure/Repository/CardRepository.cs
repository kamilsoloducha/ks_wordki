using System.Threading;
using System.Threading.Tasks;
using Cards.Domain;
using Cards.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure
{
    internal class CardRepository : ICardRepository
    {
        private readonly CardsContext _cardsContext;

        public CardRepository(CardsContext cardsContext)
        {
            _cardsContext = cardsContext;
        }

        public async Task<Card> GetCard(UserId userId, CardId cardId, CancellationToken token)
            => await _cardsContext.Cards
                .Include(x => x.Sides)
                .SingleOrDefaultAsync(x => x.Id == cardId && x.Group.CardsSet.UserId == userId, token);

        public async Task Update(CancellationToken token)
            => await _cardsContext.SaveChangesAsync(token);
    }
}