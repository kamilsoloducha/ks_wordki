using Cards.Application.Queries.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations
{
    class RepeatEntityConfiguration : IEntityTypeConfiguration<Repeat>
    {
        public void Configure(EntityTypeBuilder<Repeat> builder)
        {
            builder.ToView("repeats");
            builder.HasNoKey();
        }
    }
}