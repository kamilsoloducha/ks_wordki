using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public abstract class GetTranslation
{
    internal class QueryHandler : IRequestHandler<Query, IEnumerable<Translation>>
    {
        private readonly IDictionary _dictionary;

        public QueryHandler(IDictionary dictionary)
        {
            _dictionary = dictionary;
        }

        public async Task<IEnumerable<Translation>> Handle(Query request, CancellationToken cancellationToken)
        {
            var translation = await _dictionary.Translate(new DictionaryRequest(request.Phrase), cancellationToken);
            return translation.Translations.Select(x => new Translation(x.Definition, x.Examples));
        }
    }

    public record Query(string Phrase) : IRequest<IEnumerable<Translation>>;

    public record Translation(string Definition, IEnumerable<string> Examples);
}