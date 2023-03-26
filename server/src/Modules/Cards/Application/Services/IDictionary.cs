using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cards.Application.Services;

public interface IDictionary
{
    Task<DictionaryResponse> Translate(DictionaryRequest request, CancellationToken cancellationToken = default);
}

public record DictionaryRequest(string Word);
public record DictionaryResponse(IEnumerable<Translation> Translations);
public record Translation(string Definition, IEnumerable<string> Examples);
