using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lessons.Domain;
using Lessons.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lessons.Infrastructure
{
    internal class PerformanceRepository : IPerformanceRepository
    {
        private readonly LessonsContext _context;

        public PerformanceRepository(LessonsContext context)
        {
            _context = context;
        }

        public async Task Add(Performance newPerformance)
        {
            await _context.Performances.AddAsync(newPerformance);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Performance performance)
        {
            _context.Performances.Update(performance);

            var newLessons = performance.Lessons.Where(x => x.IsNew);
            await _context.Lessons.AddRangeAsync(newLessons);

            var newRepeats = performance.Lessons.Where(x => x.IsDirty).SelectMany(x => x.Repeats).Where(x => x.IsNew);
            await _context.Repeats.AddRangeAsync(newRepeats);

            await _context.SaveChangesAsync();
        }

        public Task<Performance> Get(PerformanceId id, CancellationToken cancellationToken)
            => _context.Performances.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public Task<Performance> GetByUserId(Guid userId, CancellationToken cancellationToken)
            => _context.Performances
                .Include(p => p.Lessons)
                .ThenInclude(l => l.Repeats)
                .SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
}