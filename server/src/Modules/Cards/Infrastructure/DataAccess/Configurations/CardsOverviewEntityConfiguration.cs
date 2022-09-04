using Cards.Application.Queries.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations;

class CardsOverviewEntityConfiguration : IEntityTypeConfiguration<CardsOverview>
{
    public void Configure(EntityTypeBuilder<CardsOverview> builder)
    {
        builder.ToView("overview");
        builder.HasNoKey();
    }
}