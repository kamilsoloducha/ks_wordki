using System.Threading;
using System.Threading.Tasks;

namespace Cards.Domain.Repositories
{
    public interface ICardRepository
    {
        Task<Card> GetCard(UserId userId, CardId cardId, CancellationToken token);
        Task Update(CancellationToken token);

    }
}