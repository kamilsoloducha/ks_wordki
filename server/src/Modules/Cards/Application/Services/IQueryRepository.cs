using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries;
using Cards.Domain;

namespace Cards.Application.Services
{
    public interface IQueryRepository
    {
        Task<IEnumerable<Repeat>> GetRepeats2(UserId userId, DateTime dateTime, int count, CancellationToken cancellationToken);
        Task<int> GetDailyRepeatsCount(UserId userId, DateTime dateTime, CancellationToken cancellationToken);
        Task<int> GetGroupsCount(UserId userId, CancellationToken cancellationToken);
        Task<int> GetCardsCount(UserId userId, CancellationToken cancellationToken);
    }
}