using Cards.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.Infrastructure
{
    internal class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");

            builder.HasKey(e => e.Id);
            builder
                .Property(e => e.Id)
                .HasColumnName(nameof(Group.Id))
                .HasConversion(
                    x => x.Value,
                    x => GroupId.Restore(x));

            builder.Property(e => e.Name)
                .HasColumnName(nameof(Group.Name))
                .HasConversion(
                    x => x.Value,
                    x => GroupName.Create(x)
                );

            builder
                .Property(e => e.FrontLanguage)
                .HasColumnName(nameof(Group.FrontLanguage))
                .HasConversion(new LanguageConverter());

            builder
                .Property(e => e.BackLanguage)
                .HasColumnName(nameof(Group.BackLanguage))
                .HasConversion(new LanguageConverter());

            builder.Property(e => e.CreationDate).HasColumnName(nameof(Group.CreationDate));

            builder.HasOne(g => g.CardsSet).WithMany(s => s.Groups);
            builder.HasMany(g => g.Cards).WithOne(c => c.Group).OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(e => e.IsNew);
            builder.Ignore(e => e.IsDirty);
        }
    }
}