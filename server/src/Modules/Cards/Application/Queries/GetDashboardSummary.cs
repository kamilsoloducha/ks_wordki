using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using Domain.Utils;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetDashboardSummary
{
    internal sealed class QueryHandler : IRequestHandler<Query, Response>
    {
        private readonly IQueryRepository _queryRepository;

        public QueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var ownerId = UserId.Restore(request.UserId);

            var dailyRepeats = await _queryRepository.GetDailyRepeatsCount(ownerId, RepeatPeriod.To, Enumerable.Empty<string>(), cancellationToken);
            var groupsCount = await _queryRepository.GetGroupsCount(ownerId, cancellationToken);
            var cardsCount = await _queryRepository.GetCardsCount(ownerId, cancellationToken);

            return new Response(groupsCount, cardsCount, dailyRepeats);
        }
    }

    public record Query(Guid UserId) : IRequest<Response>;

    public record Response(int GroupsCount, int CardsCount, int DailyRepeats);
}