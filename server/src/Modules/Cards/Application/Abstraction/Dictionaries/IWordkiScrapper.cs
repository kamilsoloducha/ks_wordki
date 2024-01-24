using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cards.Application.Abstraction.Dictionaries;

public interface IDictionary
{
    Task<IEnumerable<Translation>> Translate(string searchingTerm, CancellationToken cancellationToken = default);
}