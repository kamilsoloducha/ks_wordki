using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations;

class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("groups");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            x => x.Value,
            x => GroupId.Restore(x)
        );

        builder.Property(x => x.Name).HasConversion(
            x => x.Text,
            x => GroupName.Create(x)
        );

        builder.Property(x => x.Front).HasConversion(
            x => x.Id,
            x => Language.Create(x)
        );

        builder.Property(x => x.Back).HasConversion(
            x => x.Id,
            x => Language.Create(x)
        );

        builder.HasMany(x => x.Cards).WithMany(x => x.Groups).UsingEntity(x => x.ToTable("groups_cards"));
        builder.Navigation(x => x.Cards).Metadata.SetField("_cards");
        builder.Navigation(x => x.Cards).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}