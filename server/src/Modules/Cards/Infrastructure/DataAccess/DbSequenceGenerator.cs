using System.Threading.Tasks;
using Cards.Domain.Services;

namespace Cards.Infrastructure.DataAccess
{
    internal class DbSequenceGenerator : ISequenceGenerator
    {
        private readonly CardsContext _dbContext;

        public DbSequenceGenerator(CardsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> GenerateAsync<TType>()
        {
            var name = typeof(TType).Name;
            var sequenceName = $"{name}sequence";
            return await _dbContext.GetNextSequenceValue(sequenceName);
        }

        public long Generate<TType>()
        {
            // var test = (TType)typeof(TType).GetMethod("Restore").Invoke(null, new object[] { GenerateAsync<TType>().Result });
            return GenerateAsync<TType>().Result;
        }
    }
}