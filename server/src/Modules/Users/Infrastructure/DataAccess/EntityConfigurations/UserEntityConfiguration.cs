using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain;

namespace Users.Infrastructure.DataAccess
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Email);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.ConfirmationDate).IsRequired();
            builder.Property(x => x.LoginDate).IsRequired();
            builder.Property(x => x.FirstName);
            builder.Property(x => x.Surname);
            builder.HasMany(x => x.Roles).WithOne(x => x.User);
            builder.Ignore(x => x.Events);
            builder.Ignore(x => x.IsDirty);
            builder.Ignore(x => x.IsNew);
        }
    }
}