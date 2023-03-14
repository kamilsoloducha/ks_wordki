using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations
{
    internal class DetailsEntityConfiguration : IEntityTypeConfiguration<Details>
    {
        public void Configure(EntityTypeBuilder<Details> builder)
        {
            builder.ToTable("Details");

            builder.HasKey("CardId", "SideType");
            builder.HasIndex("CardId", "SideType");
        
            builder.Property(x => x.SideType);
        
            builder.Property(x => x.Drawer)
                .HasColumnType("SMALLINT")
                .HasConversion(
                    x => x.Correct,
                    x => new Drawer(x))
                .IsRequired();

            builder.Property(x => x.Counter)
                .HasColumnType("SMALLINT")
                .HasConversion(
                    x => x.Value,
                    x => new Counter(x))
                .IsRequired();

            builder.Property(x => x.IsQuestion);
            builder.Property(x => x.NextRepeat);

            builder.HasOne(x => x.Card).WithMany(x => x.Details);
            builder.Navigation(x => x.Card).IsRequired();
        }
    }
}