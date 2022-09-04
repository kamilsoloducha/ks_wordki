using Cards.Application.Queries.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations;

class CardDetailsEntityConfiguration : IEntityTypeConfiguration<CardSummary>
{
    public void Configure(EntityTypeBuilder<CardSummary> builder)
    {
        builder.ToView("cardsummary");
        builder.HasNoKey();
    }
}