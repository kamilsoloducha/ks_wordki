using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Abstraction;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain.Enums;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetCardSummaries
{
    internal class QueryHandler : IRequestHandler<Query, IEnumerable<CardSummaryDto>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }

        public async Task<IEnumerable<CardSummaryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cards = await _queryRepository.GetCards(new UserId(request.OwnerId), request.GroupId,
                cancellationToken);
            return cards.Select(x => Mapper.ToDto(x, _hash));
        }
    }

    public record Query(Guid OwnerId, long GroupId) : IRequest<IEnumerable<CardSummaryDto>>;
}