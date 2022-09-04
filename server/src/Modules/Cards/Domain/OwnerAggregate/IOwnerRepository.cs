using System.Threading;
using System.Threading.Tasks;
using Cards.Domain.ValueObjects;

namespace Cards.Domain.OwnerAggregate;

public interface IOwnerRepository
{
    Task<Owner> Get(OwnerId id, CancellationToken cancellationToken);
    Task Update(Owner owner, CancellationToken cancellationToken);
    Task Add(Owner owner, CancellationToken cancellationToken);

    Task<Group> GetGroup(GroupId id, CancellationToken cancellationToken);

    Task<Detail> Get(OwnerId ownerId, SideId sideId, CancellationToken cancellationToken);
    Task Update(CancellationToken cancellationToken);
}