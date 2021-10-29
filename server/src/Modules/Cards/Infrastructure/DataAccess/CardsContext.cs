using Blueprints.Infrastructure.DataAccess;
using Cards.Application.Queries;
using Cards.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Cards.Infrastructure
{
    internal class CardsContext : DbContext
    {
        private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole()
                    .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information);
                });

        private readonly string _connectionString;

        internal DbSet<Set> CardsSet { get; set; }
        internal DbSet<Group> Groups { get; set; }
        internal DbSet<Card> Cards { get; set; }
        internal DbSet<CardSide> Sides { get; set; }

        internal DbSet<Repeat> Repeats { get; set; }
        public CardsContext(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString = connectionStringProvider.ConnectrionString;
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
            modelBuilder.HasDefaultSchema("cards");
            modelBuilder.ApplyConfiguration(new SetEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CardEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CardSideEntityConfiguration());

            modelBuilder.ApplyConfiguration(new RepeatEntityConfiguration());
        }

        internal RelationalDatabaseCreator Creator => Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
    }
}