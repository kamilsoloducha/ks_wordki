﻿// <auto-generated />
using System;
using Lessons.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lessons.Infrastructure.Migrations
{
    [DbContext(typeof(LessonsContext))]
    partial class LessonsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("lessons")
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Lessons.Domain.Lesson.Lesson", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("UserId");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("StartDate");

                    b.Property<Guid?>("PerformenceId")
                        .HasColumnType("uuid");

                    b.Property<int>("TimeCounter")
                        .HasColumnType("integer")
                        .HasColumnName("TimeCounter");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("Type");

                    b.HasKey("UserId", "StartDate");

                    b.HasIndex("PerformenceId");

                    b.ToTable("Lessons", "lessons");
                });

            modelBuilder.Entity("Lessons.Domain.Performance.Performance", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.ToTable("Performances", "lessons");
                });

            modelBuilder.Entity("Lessons.Domain.Lesson.Lesson", b =>
                {
                    b.HasOne("Lessons.Domain.Performance.Performance", "Performence")
                        .WithMany("Lessons")
                        .HasForeignKey("PerformenceId");

                    b.Navigation("Performence");
                });

            modelBuilder.Entity("Lessons.Domain.Performance.Performance", b =>
                {
                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}