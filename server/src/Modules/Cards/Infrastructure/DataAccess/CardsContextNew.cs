using System;
using System.Data;
using System.Threading.Tasks;
using Blueprints.Infrastructure.DataAccess;
using Cards.Application.Queries;
using Cards.Application.Queries.Models;
using Cards.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Cards.Infrastructure2
{
    internal class CardsContextNew : DbContext
    {
        private const string Schema = "cards";
        private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole()
                    .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information);
                });

        private readonly string _connectionString;
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Detail> Details { get; set; }

        public DbSet<GroupSummary> GroupSummaries { get; set; }
        public DbSet<CardSummary> CardsDetails { get; set; }
        public DbSet<Repeat> Repeats { get; set; }
        public DbSet<RepeatCount> RepeatCounts { get; set; }

        public CardsContextNew(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString = connectionStringProvider.ConnectionString;
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
                .UseLoggerFactory(loggerFactory)
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

    public class OwnerEntityConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("owners");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                x => x.Value,
                x => OwnerId.Restore(x)
            );

            builder.HasMany(x => x.Groups).WithOne(x => x.Owner).HasForeignKey(x => x.OwnerId);
            builder.Navigation(x => x.Groups).Metadata.SetField("_groups");
            builder.Navigation(x => x.Groups).UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.Details).WithOne(x => x.Owner).HasForeignKey(x => x.OwnerId);
            builder.Navigation(x => x.Details).Metadata.SetField("_details");
            builder.Navigation(x => x.Details).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }

    public class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("groups");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(
                x => x.Value,
                x => GroupId.Restore(x)
            );

            builder.Property(x => x.Name).HasConversion(
                x => x.Text,
                x => GroupName.Create(x)
            );

            builder.Property(x => x.Front).HasConversion(
                x => x.Id,
                x => Language.Create(x)
            );

            builder.Property(x => x.Back).HasConversion(
                x => x.Id,
                x => Language.Create(x)
            );

            builder.HasMany(x => x.Cards).WithMany(x => x.Groups).UsingEntity(x => x.ToTable("groups_cards"));
            builder.Navigation(x => x.Cards).Metadata.SetField("_cards");
            builder.Navigation(x => x.Cards).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }

    public class CardEntityConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("cards");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(
                x => x.Value,
                x => CardId.Restore(x)
            );

            builder.HasOne(x => x.Front).WithMany().HasForeignKey(x => x.FrontId);
            builder.HasOne(x => x.Back).WithMany().HasForeignKey(x => x.BackId);
        }
    }

    public class SideEntityConfiguration : IEntityTypeConfiguration<Side>
    {
        public void Configure(EntityTypeBuilder<Side> builder)
        {
            builder.ToTable("sides");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(
                x => x.Value,
                x => SideId.Restore(x)
            );

            builder.Property(x => x.Value).HasConversion(
                x => x.Text,
                x => Label.Create(x)
            );
        }
    }

    public class DetailEntityConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            builder.ToTable("details");

            builder.HasKey(x => new { x.OwnerId, x.SideId });

            builder.Property(x => x.SideId).HasConversion(
                x => x.Value,
                x => SideId.Restore(x)
            );

            builder.Property(x => x.OwnerId).HasConversion(
                x => x.Value,
                x => OwnerId.Restore(x)
            );

            builder.Property(x => x.Drawer).HasConversion(
                x => x.CorrectRepeat,
                x => Drawer.Create(x)
            );

            builder.Property(x => x.NextRepeat).HasConversion(
                x => x.Date,
                x => NextRepeatMarker.Restore(x)
            );

            builder.Property(x => x.Comment).HasConversion(
                x => x.Text,
                x => Comment.Create(x)
            );
        }
    }

    public class GroupsSummaryEntityConfiguration : IEntityTypeConfiguration<GroupSummary>
    {
        public void Configure(EntityTypeBuilder<GroupSummary> builder)
        {
            builder.ToView("groupssummary");
            builder.HasNoKey();
        }
    }

    public class CardDetailsEntityConfiguration : IEntityTypeConfiguration<CardSummary>
    {
        public void Configure(EntityTypeBuilder<CardSummary> builder)
        {
            builder.ToView("cardsummary");
            builder.HasNoKey();
        }
    }

    public class RepeatEntityConfiguration : IEntityTypeConfiguration<Repeat>
    {
        public void Configure(EntityTypeBuilder<Repeat> builder)
        {
            builder.ToView("repeats");
            builder.HasNoKey();
        }
    }

    public class RepeatCountEntityConfiguration : IEntityTypeConfiguration<RepeatCount>
    {
        public void Configure(EntityTypeBuilder<RepeatCount> builder)
        {
            builder.ToView("repeatscountsummary");
            builder.HasNoKey();
        }
    }


}