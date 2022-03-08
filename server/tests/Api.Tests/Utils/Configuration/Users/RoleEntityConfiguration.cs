using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Tests.Utils
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleTest>
    {
        public void Configure(EntityTypeBuilder<RoleTest> builder)
        {
            builder.ToTable("users.Roles");
            builder.HasOne(x => x.User).WithMany(x => x.Roles);
        }
    }
}