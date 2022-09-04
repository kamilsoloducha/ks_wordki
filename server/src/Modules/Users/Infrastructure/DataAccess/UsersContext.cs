using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Users.Domain.User;
using Users.Infrastructure.DataAccess.EntityConfigurations;

namespace Users.Infrastructure.DataAccess;

internal class UsersContext : DbContext
{
    private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole()
            .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information);
    });

    private readonly string _connectionString;

    public DbSet<User> Users { get; set; }

    // public UsersContext()
    // {
    //     _connectionString = $"Host=localhost;Port=5432;Database=Wordki;User Id=root;Password=changeme;";
    // }

    public UsersContext(IConnectionStringProvider connectionStringProvider)
    {
        _connectionString = connectionStringProvider.ConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLoggerFactory(loggerFactory)
            .EnableSensitiveDataLogging()
            .UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
    }

    internal RelationalDatabaseCreator Creator
        => Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

}