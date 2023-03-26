using Cards.Domain.OwnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations;

internal class CardEntityConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("Cards");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id);
        
        builder.HasOne(x => x.Front).WithMany().HasForeignKey("FrontId");
        builder.Navigation(x => x.Front).AutoInclude();
        
        builder.HasOne(x => x.Back).WithMany().HasForeignKey("BackId");
        builder.Navigation(x => x.Back).AutoInclude();

        builder.Navigation(x => x.Details).AutoInclude();
        
        builder.Ignore(x => x.FrontDetails);
        builder.Ignore(x => x.BackDetails);
    }
}