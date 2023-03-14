using System.Threading.Tasks;

namespace Cards.Domain.Services
{
    public interface ISequenceGenerator
    {
        Task<long> GenerateAsync<TType>();
        long Generate<TType>();
    }
}