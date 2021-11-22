using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Cards.Application.Queries
{
    public class GetCard
    {
        internal class QueryHandler : IRequestHandler<Query, Response>
        {
            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public class Query : IRequest<Response>
        {
            public Guid UserId { get; set; }
            public Guid GroupId { get; set; }
            public Guid CardId { get; set; }
        }

        public class Response
        {

        }
    }
}