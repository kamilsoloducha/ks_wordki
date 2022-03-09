using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries
{
    public class GetCardsOverview
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
                var cardsOverview = await _queryRepository.GetCardsOverview(request.OwnerId, cancellationToken);

                return new Response
                {
                    All = cardsOverview.All,
                    Drawers = new[] {
                        cardsOverview.Drawer1,
                        cardsOverview.Drawer2,
                        cardsOverview.Drawer3,
                        cardsOverview.Drawer4,
                        cardsOverview.Drawer5
                    },
                    LessonIncluded = cardsOverview.LessonIncluded,
                    Ticked = cardsOverview.Ticked
                };
            }
        }

        public class Query : IRequest<Response>
        {
            public Guid OwnerId { get; set; }
        }
        public class Response
        {
            public long All { get; set; }
            public IEnumerable<long> Drawers { get; set; }
            public long LessonIncluded { get; set; }
            public long Ticked { get; set; }
        }
    }
}