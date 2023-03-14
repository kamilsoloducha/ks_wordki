using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure.DataAccess.Configurations
{
    internal class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
        
            builder.Property(x => x.Name)
                .HasConversion(
                    x => x.Text,
                    x => new GroupName(x))
                .IsRequired();

            builder.Property(x => x.ParentId);
            builder.Property(x => x.Front).IsRequired();
            builder.Property(x => x.Back).IsRequired();

            builder.HasOne(x => x.Owner).WithMany(x => x.Groups);

            builder.HasMany(x => x.Cards).WithOne(x => x.Group);
        }
    }
}