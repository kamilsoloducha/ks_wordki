using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Side> Sides { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Wordki-Test;User Id=root;Password=changeme;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Cards", "cards");

                entity.HasIndex(e => e.BackId, "IX_Cards_BackId");

                entity.HasIndex(e => e.FrontId, "IX_Cards_FrontId");

                entity.HasIndex(e => e.GroupId, "IX_Cards_GroupId");

                entity.HasIndex(e => e.Id, "IX_Cards_Id");

                entity.HasOne(d => d.Back)
                    .WithMany(p => p.CardBacks)
                    .HasForeignKey(d => d.BackId);

                entity.HasOne(d => d.Front)
                    .WithMany(p => p.CardFronts)
                    .HasForeignKey(d => d.FrontId);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.GroupId);
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.HasKey(e => new { e.CardId, e.SideType });

                entity.ToTable("Details", "cards");

                entity.HasIndex(e => new { e.CardId, e.SideType }, "IX_Details_CardId_SideType");

                entity.Property(e => e.NextRepeat).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Details)
                    .HasForeignKey(d => d.CardId);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Groups", "cards");

                entity.HasIndex(e => e.Id, "IX_Groups_Id");

                entity.HasIndex(e => e.OwnerId, "IX_Groups_OwnerId");

                entity.Property(e => e.Back).IsRequired();

                entity.Property(e => e.Front).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.OwnerId);
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.ToTable("Owners", "cards");

                entity.HasIndex(e => e.Id, "IX_Owners_Id");
            });

            modelBuilder.Entity<Side>(entity =>
            {
                entity.ToTable("Sides", "cards");

                entity.HasIndex(e => e.Id, "IX_Sides_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
