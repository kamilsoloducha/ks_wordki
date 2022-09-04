using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public class GetCards
{
    internal class QueryHandler : IRequestHandler<Query, IEnumerable<CardSummary>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }

        public async Task<IEnumerable<CardSummary>> Handle(Query request, CancellationToken cancellationToken)
        {
            var groupId = _hash.GetLongId(request.GroupId);
            var cards = await _queryRepository.GetCardSummaries(groupId, cancellationToken);

            return cards.Select(x => new CardSummary
            {
                Front = new SideSummary { Value = x.FrontValue, Example = x.FrontExample },
                Back = new SideSummary { Value = x.BackValue, Example = x.BackExample },
            });
        }
    }

    public class Query : IRequest<IEnumerable<CardSummary>>
    {
        public string GroupId { get; set; }
    }

    public class CardSummary
    {
        public SideSummary Front { get; set; }
        public SideSummary Back { get; set; }
    }

    public class SideSummary
    {
        public string Value { get; set; }
        public string Example { get; set; }
    }

}