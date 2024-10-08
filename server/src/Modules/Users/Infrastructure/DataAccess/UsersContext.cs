using Microsoft.EntityFrameworkCore;
using Users.Domain.User;

namespace Users.Infrastructure.DataAccess;

internal class UsersContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Module).Assembly);
    }
}