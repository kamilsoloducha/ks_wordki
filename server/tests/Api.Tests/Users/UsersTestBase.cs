using System.Threading.Tasks;
using Api.Tests.Model.Users;
using Microsoft.EntityFrameworkCore;
using Wordki.Tests.E2E.Feature;

namespace Api.Tests.Users
{
    public class UsersTestBase : TestBase
    {
        protected async Task ClearUsersSchema()
        {
            await using var dbContext = new UsersContext();
            await dbContext.Database.ExecuteSqlRawAsync("Delete from users.\"Roles\"");
            await dbContext.Database.ExecuteSqlRawAsync("Delete from users.\"Users\"");
        }
    }
}