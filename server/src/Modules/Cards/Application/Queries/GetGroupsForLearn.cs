using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetGroupsForLearn
{
    internal class QueryHandler : IRequestHandler<Query, IEnumerable<GroupToLessonDto>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }

        public async Task<IEnumerable<GroupToLessonDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var groups = await _queryRepository.GetGroups(request.OwnerId, cancellationToken);
            return groups.Select(x => ToDto(x, _hash));
        }

        private GroupToLessonDto ToDto(GroupToLesson group, IHashIdsService hash)
            => new (hash.GetHash(group.Id), group.Name, group.Front, group.Back, group.FrontCount, group.BackCount);
    }

    public record Query(Guid OwnerId) : IRequest<IEnumerable<GroupToLessonDto>>;
}