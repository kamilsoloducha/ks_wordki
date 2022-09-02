using System.Threading.Tasks;
using E2e.Tests;
using Microsoft.EntityFrameworkCore;
using Users.E2e.Tests.Models.Users;

namespace Users.E2e.Tests;

public class UsersTestBase : TestBase
{
    protected async Task ClearUsersSchema()
    {
        await using var dbContext = new UsersContext();
        await dbContext.Database.ExecuteSqlRawAsync("Delete from users.\"Roles\"");
        await dbContext.Database.ExecuteSqlRawAsync("Delete from users.\"Users\"");
    }
}