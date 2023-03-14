﻿// <auto-generated />
using System;
using Cards.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cards.Infrastructure.Migrations
{
    [DbContext(typeof(CardsContext))]
    partial class CardsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("cards")
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cards.Application.Queries.Models.CardsOverview", b =>
                {
                    b.Property<long>("All")
                        .HasColumnType("bigint");

                    b.Property<long>("Drawer1")
                        .HasColumnType("bigint");

                    b.Property<long>("Drawer2")
                        .HasColumnType("bigint");

                    b.Property<long>("Drawer3")
                        .HasColumnType("bigint");

                    b.Property<long>("Drawer4")
                        .HasColumnType("bigint");

                    b.Property<long>("Drawer5")
                        .HasColumnType("bigint");

                    b.Property<long>("LessonIncluded")
                        .HasColumnType("bigint");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<long>("Ticked")
                        .HasColumnType("bigint");

                    b.ToView("overview");
                });

            modelBuilder.Entity("Cards.Application.Queries.Models.CardSummary", b =>
                {
                    b.Property<string>("BackDetailsComment")
                        .HasColumnType("text");

                    b.Property<int>("BackDrawer")
                        .HasColumnType("integer");

                    b.Property<string>("BackExample")
                        .HasColumnType("text");

                    b.Property<bool>("BackIsTicked")
                        .HasColumnType("boolean");

                    b.Property<int>("BackLanguage")
                        .HasColumnType("integer");

                    b.Property<bool>("BackLessonIncluded")
                        .HasColumnType("boolean");

                    b.Property<string>("BackValue")
                        .HasColumnType("text");

                    b.Property<long>("CardId")
                        .HasColumnType("bigint");

                    b.Property<string>("FrontDetailsComment")
                        .HasColumnType("text");

                    b.Property<int>("FrontDrawer")
                        .HasColumnType("integer");

                    b.Property<string>("FrontExample")
                        .HasColumnType("text");

                    b.Property<bool>("FrontIsTicked")
                        .HasColumnType("boolean");

                    b.Property<int>("FrontLanguage")
                        .HasColumnType("integer");

                    b.Property<bool>("FrontLessonIncluded")
                        .HasColumnType("boolean");

                    b.Property<string>("FrontValue")
                        .HasColumnType("text");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("GroupName")
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.ToView("cardsummary");
                });

            modelBuilder.Entity("Cards.Application.Queries.Models.GroupSummary", b =>
                {
                    b.Property<int>("Back")
                        .HasColumnType("integer");

                    b.Property<int>("CardsCount")
                        .HasColumnType("integer");

                    b.Property<int>("Front")
                        .HasColumnType("integer");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.ToView("groupssummary");
                });

            modelBuilder.Entity("Cards.Application.Queries.Models.GroupToLesson", b =>
                {
                    b.Property<int>("Back")
                        .HasColumnType("integer");

                    b.Property<int>("BackCount")
                        .HasColumnType("integer");

                    b.Property<int>("Front")
                        .HasColumnType("integer");

                    b.Property<int>("FrontCount")
                        .HasColumnType("integer");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.ToView("grouptolesson");
                });

            modelBuilder.Entity("Cards.Application.Queries.Models.Repeat", b =>
                {
                    b.Property<string>("Answer")
                        .HasColumnType("text");

                    b.Property<string>("AnswerExample")
                        .HasColumnType("text");

                    b.Property<int>("AnswerLanguage")
                        .HasColumnType("integer");

                    b.Property<int>("AnswerType")
                        .HasColumnType("integer");

                    b.Property<int>("BackLanguage")
                        .HasColumnType("integer");

                    b.Property<long>("CardId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("FrontLanguage")
                        .HasColumnType("integer");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("GroupName")
                        .HasColumnType("text");

                    b.Property<bool>("LessonIncluded")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("NextRepeat")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.Property<int>("QuestionDrawer")
                        .HasColumnType("integer");

                    b.Property<string>("QuestionExample")
                        .HasColumnType("text");

                    b.Property<int>("QuestionLanguage")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionType")
                        .HasColumnType("integer");

                    b.Property<long>("SideId")
                        .HasColumnType("bigint");

                    b.ToView("repeats");
                });

            modelBuilder.Entity("Cards.Application.Queries.Models.RepeatCount", b =>
                {
                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.ToView("repeatscountsummary");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Card", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("BackId")
                        .HasColumnType("bigint");

                    b.Property<long?>("FrontId")
                        .HasColumnType("bigint");

                    b.Property<long?>("GroupId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BackId");

                    b.HasIndex("FrontId");

                    b.HasIndex("GroupId");

                    b.HasIndex("Id");

                    b.ToTable("Cards", "cards");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Details", b =>
                {
                    b.Property<long>("CardId")
                        .HasColumnType("bigint");

                    b.Property<int>("SideType")
                        .HasColumnType("integer");

                    b.Property<int>("Counter")
                        .HasColumnType("SMALLINT");

                    b.Property<int>("Drawer")
                        .HasColumnType("SMALLINT");

                    b.Property<bool>("IsQuestion")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsTicked")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("NextRepeat")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("CardId", "SideType");

                    b.HasIndex("CardId", "SideType");

                    b.ToTable("Details", "cards");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Back")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Front")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Groups", "cards");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Owner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Owners", "cards");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Side", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Example")
                        .HasColumnType("text");

                    b.Property<string>("Label")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Sides", "cards");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Card", b =>
                {
                    b.HasOne("Cards.Domain.OwnerAggregate.Side", "Back")
                        .WithMany()
                        .HasForeignKey("BackId");

                    b.HasOne("Cards.Domain.OwnerAggregate.Side", "Front")
                        .WithMany()
                        .HasForeignKey("FrontId");

                    b.HasOne("Cards.Domain.OwnerAggregate.Group", "Group")
                        .WithMany("Cards")
                        .HasForeignKey("GroupId");

                    b.Navigation("Back");

                    b.Navigation("Front");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Details", b =>
                {
                    b.HasOne("Cards.Domain.OwnerAggregate.Card", "Card")
                        .WithMany("Details")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Group", b =>
                {
                    b.HasOne("Cards.Domain.OwnerAggregate.Owner", "Owner")
                        .WithMany("Groups")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Card", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Group", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("Cards.Domain.OwnerAggregate.Owner", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}