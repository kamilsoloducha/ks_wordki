using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations;

internal class SideEntityConfiguration : IEntityTypeConfiguration<Side>
{
    public void Configure(EntityTypeBuilder<Side> builder)
    {
        builder.ToTable("Sides");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id);
        
        builder.Property(x => x.Label).HasConversion(
            x => x.Text,
            x => new Label(x)
        );

        builder.Property(x => x.Example).HasConversion(
            x => x.Text,
            x => new Example(x)
        );
    }
}