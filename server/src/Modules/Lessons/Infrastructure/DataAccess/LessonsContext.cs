using Blueprints.Infrastructure.DataAccess;
using Lessons.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Lessons.Infrastructure.DataAccess
{
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
        internal DbSet<Repeat> Repeats { get; set; }

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
            modelBuilder.ApplyConfiguration(new RepeatEntityConfiguration());
        }

    }
}