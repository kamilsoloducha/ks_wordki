using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations;

class CardEntityConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("cards");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            x => x.Value,
            x => CardId.Restore(x)
        );

        builder.HasOne(x => x.Front).WithMany().HasForeignKey(x => x.FrontId);
        builder.HasOne(x => x.Back).WithMany().HasForeignKey(x => x.BackId);
    }
}