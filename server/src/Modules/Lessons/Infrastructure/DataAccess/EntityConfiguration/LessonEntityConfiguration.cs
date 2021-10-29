using Lessons.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lessons.Infrastructure.DataAccess
{
    internal class LessonEntityConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");

            builder.HasKey(x => new { x.UserId, x.StartDate });
            builder.Property(x => x.UserId).HasColumnName(nameof(Lesson.UserId));
            builder.Property(x => x.StartDate).HasColumnName(nameof(Lesson.StartDate));
            builder.Property(x => x.Type).HasColumnName(nameof(Lesson.Type));
            builder.Property(x => x.TimeCounter).HasColumnName(nameof(Lesson.TimeCounter));

            builder.HasMany(x => x.Repeats).WithOne(x => x.Lesson);
            builder.HasOne(x => x.Performence).WithMany(x => x.Lessons);

            builder.Ignore(x => x.IsDirty);
            builder.Ignore(x => x.IsNew);
        }
    }
}