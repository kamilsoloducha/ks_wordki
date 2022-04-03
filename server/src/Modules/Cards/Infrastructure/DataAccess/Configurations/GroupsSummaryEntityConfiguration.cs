using Cards.Application.Queries.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations
{
    class GroupsSummaryEntityConfiguration : IEntityTypeConfiguration<GroupSummary>
    {
        public void Configure(EntityTypeBuilder<GroupSummary> builder)
        {
            builder.ToView("groupssummary");
            builder.HasNoKey();
        }
    }


}