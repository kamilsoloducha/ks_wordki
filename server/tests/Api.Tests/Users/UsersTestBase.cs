using System.Threading.Tasks;
using Wordki.Tests.E2E.Feature;

namespace Api.Tests.Users
{
    public class UsersTestBase : TestBase
    {
        protected async Task ClearUsersSchema()
        {
            using var dbContext = new TestDbContext();
            await dbContext.CleanUsersSchema();
        }
    }
}