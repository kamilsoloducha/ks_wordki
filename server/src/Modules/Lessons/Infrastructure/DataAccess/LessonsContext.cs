using Infrastructure.Services.ConnectionStringProvider;
using Lessons.Domain;
using Lessons.Domain.Lesson;
using Lessons.Domain.Performance;
using Lessons.Infrastructure.DataAccess.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Lessons.Infrastructure.DataAccess;

internal class LessonsContext : DbContext
{
    private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole()
            .AddFilter((category, level)
                => category == DbLoggerCategory.Database.Command.Name &&
                   level == LogLevel.Information);
    });

    private readonly string _connectionString;
    internal RelationalDatabaseCreator Creator
        => Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

    internal DbSet<Performance> Performances { get; set; }
    internal DbSet<Lesson> Lessons { get; set; }

    public LessonsContext()
    {
        _connectionString = $"Host=localhost;Port=5432;Database=Wordki;User Id=root;Password=changeme;";
    }

    public LessonsContext(IConnectionStringProvider connectionStringProvider)
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
        modelBuilder.HasDefaultSchema("lessons");
        modelBuilder.ApplyConfiguration(new PerformanceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new LessonEntityConfiguration());
    }

}