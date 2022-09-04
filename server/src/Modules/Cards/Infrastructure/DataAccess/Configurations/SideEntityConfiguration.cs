using Cards.Domain;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations;

class SideEntityConfiguration : IEntityTypeConfiguration<Side>
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