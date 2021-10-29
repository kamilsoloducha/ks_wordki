using Blueprints.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Users.Domain;
using Users.Infrastructure.DataAccess;

namespace Users.Infrastructure
{
    internal class UsersContext : DbContext
    {
        private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole()
            .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information);
        });

        private readonly string _connectionString;

        public DbSet<User> Users { get; set; }
        public UsersContext(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString = connectionStringProvider.ConnectrionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .UseNpgsql(_connectionString, x => x.MigrationsAssembly("Api"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        }

    }
}