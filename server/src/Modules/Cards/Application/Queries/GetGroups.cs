using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Domain;
using FluentValidation;
using MediatR;

namespace Cards.Application.Queries
{
    public class GetGroups
    {
        public class Query : IRequest<Response>
        {
            public Guid userId { get; set; }
        }

        internal class QueryHandler : IRequestHandler<Query, Response>
        {
            private readonly ISetRepository _repository;

            public QueryHandler(ISetRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = UserId.Restore(request.userId);
                var cardsSet = await _repository.Get(userId, cancellationToken);
                var groups = cardsSet.Groups.Select(CreateGroupDto);
                return new Response
                {
                    Groups = groups
                };
            }

            private GroupDto CreateGroupDto(Group group)
                => new GroupDto
                {
                    Id = group.Id.Value,
                    Name = group.Name,
                    CardsCount = group.Cards.Count,
                    CardsEnabled = group.Cards.SelectMany(x => x.Sides).Where(x => x.IsUsed).Count(),
                };
        }

        internal class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x).Must(x => x.userId != Guid.Empty);
            }
        }

        public class Response
        {
            public IEnumerable<GroupDto> Groups { get; set; }
        }

        public class GroupDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int CardsCount { get; set; }
            public int CardsEnabled { get; set; }
        }
    }
}