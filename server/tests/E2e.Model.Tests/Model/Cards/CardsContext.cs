using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace E2e.Model.Tests.Model.Cards
{
    public partial class CardsContext : DbContext
    {
        public CardsContext()
        {
        }

        public CardsContext(DbContextOptions<CardsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Cardsummary> Cardsummaries { get; set; }
        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Groupssummary> Groupssummaries { get; set; }
        public virtual DbSet<Grouptolesson> Grouptolessons { get; set; }
        public virtual DbSet<Overview> Overviews { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Repeat> Repeats { get; set; }
        public virtual DbSet<Repeatscountsummary> Repeatscountsummaries { get; set; }
        public virtual DbSet<Side> Sides { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Wordki;User Id=root;Password=changeme;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("cards", "cards");

                entity.HasIndex(e => e.BackId, "IX_cards_BackId");

                entity.HasIndex(e => e.FrontId, "IX_cards_FrontId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Back)
                    .WithMany(p => p.CardBacks)
                    .HasForeignKey(d => d.BackId);

                entity.HasOne(d => d.Front)
                    .WithMany(p => p.CardFronts)
                    .HasForeignKey(d => d.FrontId);

                entity.HasMany(d => d.Groups)
                    .WithMany(p => p.Cards)
                    .UsingEntity<Dictionary<string, object>>(
                        "GroupsCard",
                        l => l.HasOne<Group>().WithMany().HasForeignKey("GroupsId"),
                        r => r.HasOne<Card>().WithMany().HasForeignKey("CardsId"),
                        j =>
                        {
                            j.HasKey("CardsId", "GroupsId");

                            j.ToTable("groups_cards", "cards");

                            j.HasIndex(new[] { "GroupsId" }, "IX_groups_cards_GroupsId");
                        });
            });

            modelBuilder.Entity<Cardsummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cardsummary", "cards");
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.ToTable("details", "cards");

                entity.HasIndex(e => e.OwnerId, "IX_details_OwnerId");

                entity.HasIndex(e => e.SideId, "IX_details_SideId");

                entity.Property(e => e.Comment).IsRequired();

                entity.Property(e => e.NextRepeat).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Details)
                    .HasForeignKey(d => d.OwnerId);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("groups", "cards");

                entity.HasIndex(e => e.OwnerId, "IX_groups_OwnerId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.OwnerId);
            });

            modelBuilder.Entity<Groupssummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("groupssummary", "cards");
            });

            modelBuilder.Entity<Grouptolesson>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("grouptolesson", "cards");
            });

            modelBuilder.Entity<Overview>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("overview", "cards");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("owners", "cards");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Repeat>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("repeats", "cards");

                entity.Property(e => e.NextRepeat).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<Repeatscountsummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("repeatscountsummary", "cards");

                entity.Property(e => e.Date).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<Side>(entity =>
            {
                entity.ToTable("sides", "cards");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.HasSequence("cardidsequence", "cards");

            modelBuilder.HasSequence("groupidsequence", "cards");

            modelBuilder.HasSequence("sideidsequence", "cards");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
