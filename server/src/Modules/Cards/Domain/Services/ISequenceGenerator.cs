using System.Threading.Tasks;

namespace Cards.Domain
{
    public interface ISequenceGenerator
    {
        Task<long> GenerateAsync<TType>();
        long Generate<TType>();
    }
}