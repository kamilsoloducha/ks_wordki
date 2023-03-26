using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations;

internal class OwnerEntityConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("Owners");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id);

        builder.Property(x => x.UserId)
            .HasConversion(
                x => x.Value,
                x => new UserId(x)
            );

        builder.HasMany(x => x.Groups).WithOne(x => x.Owner);
    }
}