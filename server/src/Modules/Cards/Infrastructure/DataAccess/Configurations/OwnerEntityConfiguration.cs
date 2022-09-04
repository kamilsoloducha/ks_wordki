using Cards.Domain;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations;

class OwnerEntityConfiguration : IEntityTypeConfiguration<Owner>
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