using Cards.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure
{
    internal class SetEntityConfiguration : IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> builder)
        {
            builder.ToTable("Sets");

            builder.HasKey(e => e.Id);
            builder
                .Property(e => e.Id)
                .HasColumnName(nameof(Set.Id))
                .HasConversion(
                    x => x.Value,
                    x => SetId.Restore(x));

            builder
                .Property(e => e.UserId)
                .HasColumnName(nameof(Set.UserId))
                .HasConversion(
                    x => x.Value,
                    x => UserId.Restore(x)
                );

            builder.HasMany(s => s.Groups).WithOne(g => g.CardsSet).OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(x => x.Events);

            builder.Ignore(e => e.IsNew);
            builder.Ignore(e => e.IsDirty);
        }
    }
}