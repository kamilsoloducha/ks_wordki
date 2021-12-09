using Cards.Application.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure
{
    internal class RepeatCountsEntityConfiguration : IEntityTypeConfiguration<RepeatCount>
    {
        public void Configure(EntityTypeBuilder<RepeatCount> builder)
        {
            builder.ToView<RepeatCount>("repeatscountsummary");

            builder.HasNoKey();
        }

    }
}