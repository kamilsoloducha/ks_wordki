using Cards.Application.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure
{
    internal class RepeatEntityConfiguration : IEntityTypeConfiguration<Repeat>
    {
        public void Configure(EntityTypeBuilder<Repeat> builder)
        {
            builder.ToView<Repeat>("repeats");

            builder.HasNoKey();
        }
    }
}