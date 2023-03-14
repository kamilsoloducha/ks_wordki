using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain.ValueObjects;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetLanguages
{
    internal class QueryHandler : IRequestHandler<Query, IEnumerable<LanguageDto>>
    {
        private readonly IQueryRepository _queryRepository;

        public QueryHandler(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }
        
        public Task<IEnumerable<LanguageDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userId = new UserId(request.UserId);
            
            return _queryRepository.GetLanguages(userId, cancellationToken);
        }
    }

    public record Query(Guid UserId) : IRequest<IEnumerable<LanguageDto>>;

}