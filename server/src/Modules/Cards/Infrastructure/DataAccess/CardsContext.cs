using System;
using System.Data;
using System.Threading.Tasks;
using Cards.Domain.OwnerAggregate;
using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cards.Infrastructure.DataAccess;

internal class CardsContext : DbContext
{
    private const string Schema = "cards";
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILoggerFactory _loggerFactory;
    private readonly string _connectionString;

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Details> Details { get; set; }

    public CardsContext(
        IConnectionStringProvider connectionStringProvider,
        IHostEnvironment hostEnvironment,
        ILoggerFactory loggerFactory)
    {
        _hostEnvironment = hostEnvironment;
        _loggerFactory = loggerFactory;
        _connectionString = connectionStringProvider.ConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_connectionString);

        if (_hostEnvironment.IsProduction())
            return;

        optionsBuilder.UseLoggerFactory(_loggerFactory)
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CardsContext).Assembly);
    }

    internal RelationalDatabaseCreator Creator => Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

    internal async Task<long> GetNextSequenceValue(string sequenceName)
    {
        if (string.IsNullOrEmpty(sequenceName)) throw new ArgumentException(nameof(sequenceName));

        try
        {
            await using var command = Database.GetDbConnection().CreateCommand();
            command.CommandText = $"select nextval('{Schema}.{sequenceName}')";
            command.CommandType = CommandType.Text;

            await Database.OpenConnectionAsync();
            var result = await command.ExecuteReaderAsync();
            if (await result.ReadAsync())
                return Convert.ToInt64(result.GetValue(0));
            else
                throw new Exception($"An issue occured during getting sequence {sequenceName} value");
        }
        finally
        {
            await Database.CloseConnectionAsync();
        }
    }
}