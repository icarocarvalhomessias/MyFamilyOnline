﻿// <auto-generated />
using System;
using FML.Familiares.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FML.Familiares.API.Migrations
{
    [DbContext(typeof(FamiliaresContext))]
    [Migration("20241205171559_Familiar")]
    partial class Familiar
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Familiar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeathDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FamilyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FatherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<Guid>("HouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Matriarch")
                        .HasColumnType("bit");

                    b.Property<Guid?>("MotherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Patriarch")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SecretSanta")
                        .HasColumnType("bit");

                    b.Property<Guid?>("Spouse")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.HasIndex("HouseId");

                    b.HasIndex("MotherId");

                    b.HasIndex("Spouse")
                        .IsUnique()
                        .HasFilter("[Spouse] IS NOT NULL");

                    b.ToTable("Relatives");
                });

            modelBuilder.Entity("Family", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("History")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("House", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FamilyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("Familiar", b =>
                {
                    b.HasOne("Family", "Family")
                        .WithMany("Relatives")
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("House", "House")
                        .WithMany("Residents")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Familiar", null)
                        .WithMany("Children")
                        .HasForeignKey("MotherId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Familiar", "SpouseObj")
                        .WithOne()
                        .HasForeignKey("Familiar", "Spouse");

                    b.Navigation("Family");

                    b.Navigation("House");

                    b.Navigation("SpouseObj");
                });

            modelBuilder.Entity("House", b =>
                {
                    b.HasOne("Family", null)
                        .WithMany("Houses")
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Familiar", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Family", b =>
                {
                    b.Navigation("Houses");

                    b.Navigation("Relatives");
                });

            modelBuilder.Entity("House", b =>
                {
                    b.Navigation("Residents");
                });
#pragma warning restore 612, 618
        }
    }
}
