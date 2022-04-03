using Cards.Application.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations
{
    class GroupsToLessonEntityConfiguration : IEntityTypeConfiguration<GroupToLesson>
    {
        public void Configure(EntityTypeBuilder<GroupToLesson> builder)
        {
            builder.ToView("grouptolesson");
            builder.HasNoKey();
        }
    }


}