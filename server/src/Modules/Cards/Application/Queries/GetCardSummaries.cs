using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries
{

    public class GetCardSummaries
    {
        internal class QueryHandler : IRequestHandler<Query, Response>
        {
            private readonly IQueryRepository _queryRepository;

            public QueryHandler(IQueryRepository queryRepository)
            {
                _queryRepository = queryRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var cards = await _queryRepository.GetCardSummaries(request.OwnerId, request.GroupId, cancellationToken);
                return new Response
                {
                    Cards = cards.Select(x => x.ToDto())
                };
            }
        }

        public class Query : IRequest<Response>
        {
            public Guid OwnerId { get; set; }
            public long GroupId { get; set; }
        }

        public class Response
        {
            public IEnumerable<CardSummary> Cards { get; set; }
        }

        public class CardSummary
        {
            public long Id { get; set; }
            public SideSummary Back { get; set; }
            public SideSummary Front { get; set; }
        }

        public class SideSummary
        {
            public int Type { get; set; }
            public string Value { get; set; }
            public string Example { get; set; }
            public string Comment { get; set; }
            public int Drawer { get; set; }
            public bool IsUsed { get; set; }
            public bool IsTicked { get; set; }
        }
    }
}