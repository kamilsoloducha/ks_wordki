using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public class GetGroupDetails
{
    internal class QueryHandler : IRequestHandler<Query, Response>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var groupId = _hash.GetLongId(request.GroupId);
            var groupDetails = await _queryRepository.GetGroupDetails(groupId, cancellationToken);
            return new Response
            {
                Id = _hash.GetHash(groupDetails.Id),
                Name = groupDetails.Name,
                Front = groupDetails.Front,
                Back = groupDetails.Back
            };
        }
    }
    public class Query : IRequest<Response>
    {
        public string GroupId { get; set; }
    }

    public class Response
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Front { get; set; }
        public int Back { get; set; }
    }
}