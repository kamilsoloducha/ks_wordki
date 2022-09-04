using System;
using System.Data;
using System.Threading.Tasks;
using Cards.Application.Queries;
using Cards.Application.Queries.Models;
using Cards.Domain;
using Cards.Domain.OwnerAggregate;
using Cards.Infrastructure.DataAccess.Configurations;
using Infrastructure.Services.ConnectionStringProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Cards.Infrastructure.DataAccess;

internal class CardsContext : DbContext
{
    private const string Schema = "cards";
    private readonly ILoggerFactory _loggerFactory;
    private readonly string _connectionString;

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Detail> Details { get; set; }

    public DbSet<GroupSummary> GroupSummaries { get; set; }
    public DbSet<CardSummary> CardsDetails { get; set; }
    public DbSet<Repeat> Repeats { get; set; }
    public DbSet<RepeatCount> RepeatCounts { get; set; }
    public DbSet<GroupToLesson> GroupsToLesson { get; set; }
    public DbSet<CardsOverview> CardsOverviews { get; set; }

    public CardsContext(IConnectionStringProvider connectionStringProvider, ILoggerFactory loggerFactory)
    {
        _connectionString = connectionStringProvider.ConnectionString;
        _loggerFactory = loggerFactory;
    }

    // public CardsContextNew(IConnectionStringProvider connectionStringProvider, ILogger<CardsContextNew> logger)
    // {
    //     _connectionString = connectionStringProvider.ConnectionString;
    //     _connectionStringProvider = connectionStringProvider;
    //     _logger = logger;
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLoggerFactory(_loggerFactory)
            .EnableSensitiveDataLogging()
            .UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfiguration(new OwnerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CardEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SideEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DetailEntityConfiguration());

        modelBuilder.ApplyConfiguration(new GroupsSummaryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CardDetailsEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RepeatEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RepeatCountEntityConfiguration());
        modelBuilder.ApplyConfiguration(new GroupsToLessonEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CardsOverviewEntityConfiguration());

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