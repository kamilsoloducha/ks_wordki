using Cards.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure
{
    internal class CardSideEntityConfiguration : IEntityTypeConfiguration<CardSide>
    {
        public void Configure(EntityTypeBuilder<CardSide> builder)
        {
            builder.ToTable("CardSides");

            builder.HasKey(e => new { e.CardId, e.Side });

            builder.Property(e => e.Value)
            .HasColumnName(nameof(CardSide.Value))
            .HasConversion(
                x => x.Value,
                x => SideLabel.Create(x)
            );

            builder.Property(e => e.Example).HasColumnName(nameof(CardSide.Example));

            builder.Property(e => e.Drawer)
                .HasColumnName(nameof(CardSide.Drawer))
                .HasConversion(new DrawerConverter());

            builder.Property(e => e.Repeats)
                .HasColumnName(nameof(CardSide.Repeats))
                .HasConversion(
                    x => x.Value,
                    x => RepeatCounter.Create(x));

            builder.Property(e => e.IsUsed).HasColumnName(nameof(CardSide.IsUsed));

            builder.Property(e => e.NextRepeat)
                .HasColumnName(nameof(CardSide.NextRepeat))
                .HasConversion(
                        x => x.Value,
                        x => NextRepeat.Restore(x));

            builder.Property(e => e.Side).HasColumnName(nameof(CardSide.Side));

            builder.HasOne(s => s.Card).WithMany(c => c.Sides).HasForeignKey("CardId");

            builder.Ignore(e => e.IsNew);
            builder.Ignore(e => e.IsDirty);
        }
    }
}