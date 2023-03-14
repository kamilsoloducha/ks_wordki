using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetCards
{
    internal class QueryHandler : IRequestHandler<Query, IEnumerable<CardSummary>>
    {
        private readonly IQueryRepository _queryRepository;

        public QueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<IEnumerable<CardSummary>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cards = await _queryRepository.GetCardSummaries(request.GroupId, cancellationToken);

            return cards.Select(x => new CardSummary
            {
                Front = new SideSummary { Value = x.FrontValue, Example = x.FrontExample },
                Back = new SideSummary { Value = x.BackValue, Example = x.BackExample },
            });
        }
    }

    public record Query(long GroupId) : IRequest<IEnumerable<CardSummary>>;

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