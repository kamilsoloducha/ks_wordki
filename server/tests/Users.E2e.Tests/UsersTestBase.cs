using System.Threading.Tasks;
using E2e.Model.Tests.Model.Users;
using E2e.Tests;
using Microsoft.EntityFrameworkCore;

namespace Users.E2e.Tests
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