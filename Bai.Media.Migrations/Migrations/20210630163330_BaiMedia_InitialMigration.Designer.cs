﻿// <auto-generated />
using System;
using Bai.Media.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bai.Media.Migrations.Migrations
{
    [DbContext(typeof(MediaDbContext))]
    [Migration("20210630163330_BaiMedia_InitialMigration")]
    partial class BaiMedia_InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bai.Media.DAL.Models.Avatar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UniqueIdentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreatedDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<Guid>("FileExtension")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<int>("FileSize")
                        .HasColumnType("Int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("UniqueIdentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Avatars", "dbo");
                });

            modelBuilder.Entity("Bai.Media.DAL.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UniqueIdentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreatedDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<Guid>("FileExtension")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<int>("FileSize")
                        .HasColumnType("Int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("UniqueIdentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Image", "dbo");
                });
#pragma warning restore 612, 618
        }
    }
}
