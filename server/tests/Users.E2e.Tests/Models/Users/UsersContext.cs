﻿#nullable disable

using Microsoft.EntityFrameworkCore;

namespace Users.E2e.Tests.Models.Users;

public partial class UsersContext : DbContext
{
    public UsersContext()
    {
    }

    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=wordki-test;User Id=root;Password=changeme;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles", "users");

            entity.HasIndex(e => e.UserId, "IX_Roles_UserId");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Roles)
                .HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "users");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name).IsRequired();

            entity.Property(e => e.Password).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}