using Cards.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure
{
    internal class CardEntityConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName(nameof(Card.Id))
                .HasConversion(x => x.Value, x => CardId.Restore(x));

            builder.Property(e => e.Comment).HasColumnName(nameof(Card.Comment));
            builder.Property(e => e.IsTicked).HasColumnName(nameof(Card.IsTicked));
            builder.Property(e => e.CreationDate).HasColumnName(nameof(Card.CreationDate));

            builder.HasMany(c => c.Sides).WithOne(s => s.Card).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Group).WithMany(s => s.Cards);

            builder.Ignore(e => e.IsNew);
            builder.Ignore(e => e.IsDirty);
        }
    }
}