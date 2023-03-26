using System.Threading;
using System.Threading.Tasks;

namespace Domain.Rules;

public interface IBuissnessRule
{
    Task<bool> IsCorrect(CancellationToken cancellationToken);
    string Message { get; }
}