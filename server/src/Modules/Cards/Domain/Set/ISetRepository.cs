using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cards.Domain
{
    public interface ISetRepository
    {
        Task<Set> Get(UserId userId, CancellationToken cancellationToken);
        Task Add(Set cardsSet, CancellationToken cancellationToken);
        Task Update(Set cardsSet, CancellationToken cancellationToken);
    }
}