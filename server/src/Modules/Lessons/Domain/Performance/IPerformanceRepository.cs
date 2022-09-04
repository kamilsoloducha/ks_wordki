using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lessons.Domain.Performance;

public interface IPerformanceRepository
{
    Task Add(Performance newPerformance);
    Task Update(Performance performance);
    Task<Performance> Get(PerformanceId id, CancellationToken cancellationToken);
    Task<Performance> GetByUserId(Guid userId, CancellationToken cancellationToken);
}