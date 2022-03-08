using System.Threading.Tasks;
using Api.Tests.Utils;
using Microsoft.EntityFrameworkCore;

namespace Api.Tests
{
    internal class TestDbContext : DbContext
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Database=WordkiTest;User Id=root;Password=changeme;";

        public TestDbContext() { }

        public DbSet<UserTest> Users { get; set; }
        public DbSet<RoleTest> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }

        public async Task CleanUsersSchema()
        {
            await Database.ExecuteSqlRawAsync("DELETE FROM users.\"Roles\"");
            await Database.ExecuteSqlRawAsync("DELETE FROM users.\"Users\"");
        }
    }
}