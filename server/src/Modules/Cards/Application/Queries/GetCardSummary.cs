using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Abstraction;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain.Enums;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetCardSummary
{
    internal class QueryHandler : IRequestHandler<Query, CardSummaryDto>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }

        public async Task<CardSummaryDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var card = await _queryRepository.GetCard(new UserId(request.OwnerId), request.CardId, cancellationToken);
            return Mapper.ToDto(card, _hash);
        }
    }

    public record Query(Guid OwnerId, long CardId) : IRequest<CardSummaryDto>;
}