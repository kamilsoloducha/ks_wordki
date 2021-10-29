using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lessons.Domain
{
    public interface IPerformanceRepository
    {
        Task Add(Performance newPerformance);
        Task<Performance> Get(PerformanceId id, CancellationToken cancellationToken);
        Task<Performance> GetByUserId(Guid userId, CancellationToken cancellationToken);
    }
}