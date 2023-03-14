using Lessons.Domain.Performance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lessons.Infrastructure.DataAccess.EntityConfiguration
{
    internal class PerformanceEntityConfiguration : IEntityTypeConfiguration<Performance>
    {
        public void Configure(EntityTypeBuilder<Performance> builder)
        {
            builder.ToTable("Performances");

            builder.HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasColumnName(nameof(Performance.Id))
                .HasConversion(
                    x => x.Value,
                    x => PerformanceId.Restore(x));

            builder.Property(x => x.UserId).HasColumnName(nameof(Performance.UserId));
            builder.HasMany(x => x.Lessons).WithOne(x => x.Performence);

            builder.Ignore(x => x.IsDirty);
            builder.Ignore(x => x.IsNew);
        }
    }
}