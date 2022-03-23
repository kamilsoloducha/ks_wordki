using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Api.Tests.Model.Cards
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
        public virtual DbSet<GroupsCard> GroupsCards { get; set; }
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
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Wordki;User Id=root;Password=changeme;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

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
            });

            modelBuilder.Entity<Cardsummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cardsummary", "cards");
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.HasKey(e => new { e.OwnerId, e.SideId });

                entity.ToTable("details", "cards");

                entity.HasIndex(e => e.OwnerId, "IX_details_OwnerId");

                entity.HasIndex(e => e.SideId, "IX_details_SideId");

                entity.Property(e => e.Comment).IsRequired();

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

            modelBuilder.Entity<GroupsCard>(entity =>
            {
                entity.HasKey(e => new { e.CardsId, e.GroupsId });

                entity.ToTable("groups_cards", "cards");

                entity.HasIndex(e => e.GroupsId, "IX_groups_cards_GroupsId");

                entity.HasOne(d => d.Cards)
                    .WithMany(p => p.GroupsCards)
                    .HasForeignKey(d => d.CardsId);

                entity.HasOne(d => d.Groups)
                    .WithMany(p => p.GroupsCards)
                    .HasForeignKey(d => d.GroupsId);
            });

            modelBuilder.Entity<Groupssummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("groupssummary", "cards");
            });

            modelBuilder.Entity<Grouptolesson>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("grouptolesson", "cards");
            });

            modelBuilder.Entity<Overview>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("overview", "cards");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("owners", "cards");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Repeat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("repeats", "cards");
            });

            modelBuilder.Entity<Repeatscountsummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("repeatscountsummary", "cards");
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
