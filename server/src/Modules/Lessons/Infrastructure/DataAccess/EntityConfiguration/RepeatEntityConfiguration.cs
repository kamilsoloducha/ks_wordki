using Lessons.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lessons.Infrastructure.DataAccess
{
    internal class RepeatEntityConfiguration : IEntityTypeConfiguration<Repeat>
    {
        public void Configure(EntityTypeBuilder<Repeat> builder)
        {
            builder.ToTable("Repeats");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.CardId).HasColumnName(nameof(Repeat.CardId));
            builder.Property(x => x.RepeatDate).HasColumnName(nameof(Repeat.RepeatDate));
            builder.Property(x => x.Result).HasColumnName(nameof(Repeat.Result));
            builder.Property(x => x.Side).HasColumnName(nameof(Repeat.Lesson));

            builder.HasOne(x => x.Lesson).WithMany(x => x.Repeats);

            builder.Ignore(x => x.IsDirty);
            builder.Ignore(x => x.IsNew);
        }
    }
}