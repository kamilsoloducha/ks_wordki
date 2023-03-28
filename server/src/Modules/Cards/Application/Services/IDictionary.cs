using System.Threading;
using System.Threading.Tasks;

namespace Cards.Application.Services;

public interface IDictionary
{
    Task<DictionaryResponse> Translate(DictionaryRequest request, CancellationToken cancellationToken = default);
}