using Cards.Domain;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations;

class DetailEntityConfiguration : IEntityTypeConfiguration<Detail>
{
    public void Configure(EntityTypeBuilder<Detail> builder)
    {
        builder.ToTable("details");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SideId).HasConversion(
            x => x.Value,
            x => SideId.Restore(x)
        );

        builder.Property(x => x.OwnerId).HasConversion(
            x => x.Value,
            x => OwnerId.Restore(x)
        );

        builder.Property(x => x.Drawer).HasConversion(
            x => x.CorrectRepeat,
            x => Drawer.Create(x)
        );

        builder.Property(x => x.NextRepeat).HasConversion(
            x => x.Date,
            x => NextRepeatMarker.Restore(x)
        );

        builder.Property(x => x.Comment).HasConversion(
            x => x.Text,
            x => Comment.Create(x)
        );
    }
}