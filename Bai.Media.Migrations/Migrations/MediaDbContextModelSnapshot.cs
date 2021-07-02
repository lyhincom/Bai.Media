﻿// <auto-generated />
using System;
using Bai.Media.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bai.Media.Migrations.Migrations
{
    [DbContext(typeof(MediaDbContext))]
    partial class MediaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bai.Media.DAL.Models.AvatarEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UniqueIdentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("NVarChar(30)");

                    b.Property<DateTime>("CreatedDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("DatabaseUrl")
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Bit")
                        .HasDefaultValue(false);

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("NVarChar(10)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("BigInt");

                    b.Property<string>("FileSystemUrl")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Height")
                        .HasColumnType("Int");

                    b.Property<byte[]>("ImageBytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<int>("Width")
                        .HasColumnType("Int");

                    b.HasKey("Id")
                        .HasName("PK_Avatar_Id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("IX_Avatar_Key");

                    b.HasIndex("UserId", "FileExtension", "FileSizeInBytes", "CreatedDt", "Deleted")
                        .HasDatabaseName("IX_Avatar_QueryFields");

                    b.ToTable("Avatars", "dbo");
                });

            modelBuilder.Entity("Bai.Media.DAL.Models.ImageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UniqueIdentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("NVarChar(30)");

                    b.Property<DateTime>("CreatedDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("DatabaseUrl")
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Bit")
                        .HasDefaultValue(false);

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("NVarChar(10)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("BigInt");

                    b.Property<string>("FileSystemUrl")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Height")
                        .HasColumnType("Int");

                    b.Property<byte[]>("ImageBytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("ImageGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PageId")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<string>("PageType")
                        .IsRequired()
                        .HasColumnType("NVarChar(100)");

                    b.Property<int?>("Priority")
                        .HasColumnType("Int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<int>("Width")
                        .HasColumnType("Int");

                    b.HasKey("Id")
                        .HasName("PK_Image_Id");

                    b.HasIndex("UserId", "PageId", "PageType")
                        .HasDatabaseName("IX_Image_Key");

                    b.HasIndex("UserId", "PageId", "PageType", "ImageGroupId", "Priority")
                        .HasDatabaseName("IX_Image_ImageGroupPriority");

                    b.HasIndex("UserId", "PageId", "PageType", "FileExtension", "FileSizeInBytes", "CreatedDt", "Deleted")
                        .HasDatabaseName("IX_Image_QueryFields");

                    b.ToTable("Image", "dbo");
                });

            modelBuilder.Entity("Bai.Media.DAL.Models.LogoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UniqueIdentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("NVarChar(30)");

                    b.Property<DateTime>("CreatedDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("DatabaseUrl")
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Bit")
                        .HasDefaultValue(false);

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("NVarChar(10)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("BigInt");

                    b.Property<string>("FileSystemUrl")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Height")
                        .HasColumnType("Int");

                    b.Property<byte[]>("ImageBytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("PageId")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<int>("Width")
                        .HasColumnType("Int");

                    b.HasKey("Id")
                        .HasName("PK_Logo_Id");

                    b.HasIndex("PageId")
                        .HasDatabaseName("IX_Logo_Key");

                    b.HasIndex("PageId", "FileExtension", "FileSizeInBytes", "CreatedDt", "Deleted")
                        .HasDatabaseName("IX_Logo_QueryFields");

                    b.ToTable("Logo", "dbo");
                });
#pragma warning restore 612, 618
        }
    }
}
