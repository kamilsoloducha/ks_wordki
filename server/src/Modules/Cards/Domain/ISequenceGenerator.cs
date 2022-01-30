using System.Threading.Tasks;

namespace Cards.Domain2
{
    public interface ISequenceGenerator
    {
        Task<long> GenerateAsync<TType>();
        long Generate<TType>();
    }
}