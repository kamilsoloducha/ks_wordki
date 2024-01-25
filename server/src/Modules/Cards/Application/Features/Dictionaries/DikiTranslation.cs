using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Abstraction.Dictionaries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using IDictionary = Cards.Application.Abstraction.Dictionaries.IDictionary;

namespace Cards.Application.Features.Dictionaries;

public abstract class DikiTranslation
{
    internal class Handler([FromKeyedServices("Diki")] IDictionary dikiDictionary)
        : IRequestHandler<Query, IEnumerable<Translation>>
    {
        public Task<IEnumerable<Translation>> Handle(Query request, CancellationToken cancellationToken)
        {
            var searchingTerm = request.SearchingTerm;
            return dikiDictionary.Translate(searchingTerm, cancellationToken);
        }
    }

    public record Query(string SearchingTerm) : IRequest<IEnumerable<Translation>>;
}