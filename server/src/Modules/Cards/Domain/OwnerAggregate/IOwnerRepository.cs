using System.Threading;
using System.Threading.Tasks;
using Cards.Domain.ValueObjects;

namespace Cards.Domain.OwnerAggregate
{
    public interface IOwnerRepository
    {
        Task<Owner> Get(UserId id, CancellationToken cancellationToken);
    
        Task<Group> GetGroup(UserId userId, long id, CancellationToken cancellationToken);
        Task<Group> GetGroup(long id, CancellationToken cancellationToken);
    
        Task<Card> GetCard(UserId userId, long id, CancellationToken cancellationToken);
    
    
        Task Update(CancellationToken cancellationToken);
        Task Add(Owner owner, CancellationToken cancellationToken);
    }
}