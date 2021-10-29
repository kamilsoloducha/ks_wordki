using System.Threading;
using System.Threading.Tasks;

namespace Blueprints.Domain
{
    public interface IBuissnessRule
    {
        Task<bool> IsCorrect(CancellationToken cancellationToken);
        string Message { get; }
    }
}
