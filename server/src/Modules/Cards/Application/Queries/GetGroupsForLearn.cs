using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries
{
    public class GetGroupsForLearn
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
                var groups = await _queryRepository.GetGroups(request.OwnerId, cancellationToken);
                return new Response
                {
                    Groups = groups.Select(x => ToDto(x, _hash)),
                };
            }

            private GroupToLessonDto ToDto(GroupToLesson group, IHashIdsService hash)
                => new GroupToLessonDto
                {
                    Id = hash.GetHash(group.Id),
                    Name = group.Name,
                    Front = group.Front,
                    Back = group.Back,
                    FrontCount = group.FrontCount,
                    BackCount = group.BackCount
                };
        }

        public class Query : IRequest<Response>
        {
            public Guid OwnerId { get; set; }
        }

        public class Response
        {
            public IEnumerable<GroupToLessonDto> Groups { get; set; }
        }

        public class GroupToLessonDto
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Front { get; set; }
            public int Back { get; set; }
            public int FrontCount { get; set; }
            public int BackCount { get; set; }
        }
    }
}