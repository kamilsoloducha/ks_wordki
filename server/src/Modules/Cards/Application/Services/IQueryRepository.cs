using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries;
using Cards.Domain;

namespace Cards.Application.Services
{
    public interface IQueryRepository
    {
        Task<List<CardSide>> GetRepeats(UserId userId, int count, CancellationToken cancellationToken);

        Task<IEnumerable<Repeat>> GetRepeats2(UserId userId, int count, CancellationToken cancellationToken);
    }
}