using Cards.Application.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations
{
    class RepeatCountEntityConfiguration : IEntityTypeConfiguration<RepeatCount>
    {
        public void Configure(EntityTypeBuilder<RepeatCount> builder)
        {
            builder.ToView("repeatscountsummary");
            builder.HasNoKey();
        }
    }


}