﻿// <auto-generated />
using System;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(SurveyAppDbContext))]
    [Migration("20240519163121_init2")]
    partial class init2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description");

                    b.Property<int>("Order")
                        .HasColumnType("int")
                        .HasColumnName("order");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(50)")
                        .HasDefaultValue("checkbox")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("option", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("date")
                        .HasColumnName("due_date");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("question");

                    b.Property<Settings>("Settings")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("settings");

                    b.HasKey("Id");

                    b.ToTable("survey", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ip_address");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("vote", (string)null);
                });

            modelBuilder.Entity("OptionVote", b =>
                {
                    b.Property<int>("OptionsId")
                        .HasColumnType("int");

                    b.Property<int>("VotesId")
                        .HasColumnType("int");

                    b.HasKey("OptionsId", "VotesId");

                    b.HasIndex("VotesId");

                    b.ToTable("OptionVote");
                });

            modelBuilder.Entity("Domain.Entities.Option", b =>
                {
                    b.HasOne("Domain.Entities.Survey", "Survey")
                        .WithMany("Options")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Domain.Entities.Vote", b =>
                {
                    b.HasOne("Domain.Entities.Survey", "Survey")
                        .WithMany("Votes")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("OptionVote", b =>
                {
                    b.HasOne("Domain.Entities.Option", null)
                        .WithMany()
                        .HasForeignKey("OptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Vote", null)
                        .WithMany()
                        .HasForeignKey("VotesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Survey", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
