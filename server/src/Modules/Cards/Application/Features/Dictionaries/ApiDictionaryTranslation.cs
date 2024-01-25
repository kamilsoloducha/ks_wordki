using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Abstraction.Dictionaries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Application.Features.Dictionaries;

public abstract class ApiDictionaryTranslation
{
    internal class Handler([FromKeyedServices("ApiDictionary")] IDictionary dictionary) 
        : IRequestHandler<Query, IEnumerable<Translation>>
    {
        public Task<IEnumerable<Translation>> Handle(Query request, CancellationToken cancellationToken) 
            => dictionary.Translate(request.Phrase, cancellationToken);
    }

    public record Query(string Phrase) : IRequest<IEnumerable<Translation>>;
}