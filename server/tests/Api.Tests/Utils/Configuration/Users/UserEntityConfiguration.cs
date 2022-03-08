using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Tests.Utils
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserTest>
    {
        public void Configure(EntityTypeBuilder<UserTest> builder)
        {
            builder.ToTable("Users");
        }
    }
}