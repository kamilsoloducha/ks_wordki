using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Queries;
using Cards.Application.Queries.Models;
using Cards.Domain;

namespace Cards.Application.Services
{
    public interface IQueryRepository
    {
        Task<IEnumerable<Repeat>> GetRepeats(OwnerId ownerId, DateTime dateTime, int count, int questionLanguage, CancellationToken cancellationToken);
        Task<int> GetDailyRepeatsCount(OwnerId ownerId, DateTime dateTime, int questionLanguage, CancellationToken cancellationToken);
        Task<int> GetNewRepeatsCount(OwnerId ownerId, int questionLanguage, long? groupId, CancellationToken cancellationToken);
        Task<int> GetGroupsCount(OwnerId ownerId, CancellationToken cancellationToken);
        Task<int> GetCardsCount(OwnerId ownerId, CancellationToken cancellationToken);
        Task<IEnumerable<RepeatCount>> GetRepeatsCountSummary(OwnerId ownerId, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken);


        Task<IEnumerable<GroupSummary>> GetGroupSummaries(Guid ownerId, CancellationToken cancellationToken);
        Task<IEnumerable<CardSummary>> GetCardSummaries(Guid ownerId, long groupId, CancellationToken cancellationToken);
        Task<GroupSummary> GetGroupDetails(long groupId, CancellationToken cancellationToken);
    }
}