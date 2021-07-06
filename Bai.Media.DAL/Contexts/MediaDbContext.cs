using Bai.General.DAL.Extensions;
using Bai.Media.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Bai.Media.DAL.Contexts
{
    public class MediaDbContext : DbContext
    {
        public MediaDbContext(DbContextOptions<MediaDbContext> options)
            : base(options)
        {
        }

        public DbSet<AvatarEntity> Avatars { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // dbo.Avatar
            builder.Entity<AvatarEntity>(indexOptions =>
            {
                indexOptions.ToTable("Avatars", "dbo");
                indexOptions.HasKey(avatar => avatar.Id)
                            .HasName("PK_Avatar_Id");

                indexOptions.HasIndex(avatar => new { avatar.UserId })
                            .HasDatabaseName("IX_Avatar_Key");
                indexOptions.HasIndex(avatar => new { avatar.UserId,
                                                      avatar.FileExtension, avatar.FileSizeInBytes, avatar.CreatedDt, avatar.Deleted })
                            .HasDatabaseName("IX_Avatar_QueryFields");
            });
            builder.Entity<AvatarEntity>(propertyOptions =>
            {
                propertyOptions.PropertyDefault(x => x.Id).HasDefaultValueSql("NEWID()");
                
                propertyOptions.PropertyDefault(x => x.UserId);
                
                propertyOptions.PropertySizeDefault(x => x.FileExtension, 10);
                propertyOptions.PropertyDefault(x => x.FileSizeInBytes);
                propertyOptions.PropertyDefault(x => x.Width);
                propertyOptions.PropertyDefault(x => x.Height);
                propertyOptions.PropertySizeDefault(x => x.ContentType, 30);
                propertyOptions.Property(x => x.ImageBytes).HasColumnType("varbinary(max)").IsRequired();
                propertyOptions.PropertyDefault(x => x.CreatedDt).HasDefaultValueSql("GetUtcDate()");
                propertyOptions.PropertyDefault(x => x.Deleted).HasDefaultValue(false);
                propertyOptions.Property(x => x.DatabaseUrl).HasColumnType("nvarchar(200)");
                propertyOptions.Property(x => x.FileSystemUrl).HasColumnType("nvarchar(200)");
            });

            // dbo.Image
            builder.Entity<ImageEntity>(indexOptions =>
            {
                indexOptions.ToTable("Image", "dbo");
                indexOptions.HasKey(image => image.Id)
                            .HasName("PK_Image_Id");

                indexOptions.HasIndex(image => new { image.PageId, image.PageType })
                            .HasDatabaseName("IX_Image_Key");
                indexOptions.HasIndex(image => new { image.PageId, image.PageType,
                                                     image.ImageGroupId, image.Priority })
                            .HasDatabaseName("IX_Image_ImageGroupPriority");
                indexOptions.HasIndex(image => new { image.PageId, image.PageType,
                                                     image.FileExtension, image.FileSizeInBytes, image.CreatedDt, image.Deleted })
                            .HasDatabaseName("IX_Image_QueryFields");
            });
            builder.Entity<ImageEntity>(propertyOptions =>
            {
                propertyOptions.PropertyDefault(x => x.Id).HasDefaultValueSql("NEWID()");
                
                propertyOptions.PropertyDefault(x => x.PageId);
                propertyOptions.PropertySizeDefault(x => x.PageType, 100);
                propertyOptions.Property(x => x.ImageGroupId).HasColumnType("uniqueidentifier");
                propertyOptions.PropertyDefault(x => x.Priority);

                propertyOptions.PropertySizeDefault(x => x.FileExtension, 10);
                propertyOptions.PropertyDefault(x => x.FileSizeInBytes);
                propertyOptions.PropertyDefault(x => x.Width);
                propertyOptions.PropertyDefault(x => x.Height);
                propertyOptions.PropertySizeDefault(x => x.ContentType, 30);
                propertyOptions.Property(x => x.ImageBytes).HasColumnType("varbinary(max)").IsRequired();
                propertyOptions.PropertyDefault(x => x.CreatedDt).HasDefaultValueSql("GetUtcDate()");
                propertyOptions.PropertyDefault(x => x.Deleted).HasDefaultValue(false);
                propertyOptions.Property(x => x.DatabaseUrl).HasColumnType("nvarchar(200)");
                propertyOptions.Property(x => x.FileSystemUrl).HasColumnType("nvarchar(200)");
            });

            // dbo.Logo
            builder.Entity<LogoEntity>(indexOptions =>
            {
                indexOptions.ToTable("Logo", "dbo");
                indexOptions.HasKey(logo => logo.Id)
                            .HasName("PK_Logo_Id");

                indexOptions.HasIndex(logo => new { logo.PageId })
                            .HasDatabaseName("IX_Logo_Key");
                indexOptions.HasIndex(logo => new { logo.PageId,
                                                    logo.FileExtension, logo.FileSizeInBytes, logo.CreatedDt, logo.Deleted })
                            .HasDatabaseName("IX_Logo_QueryFields");
            });
            builder.Entity<LogoEntity>(propertyOptions =>
            {
                propertyOptions.PropertyDefault(x => x.Id).HasDefaultValueSql("NEWID()");
                
                propertyOptions.PropertyDefault(x => x.PageId);

                propertyOptions.PropertySizeDefault(x => x.FileExtension, 10);
                propertyOptions.PropertyDefault(x => x.FileSizeInBytes);
                propertyOptions.PropertyDefault(x => x.Width);
                propertyOptions.PropertyDefault(x => x.Height);
                propertyOptions.PropertySizeDefault(x => x.ContentType, 30);
                propertyOptions.Property(x => x.ImageBytes).HasColumnType("varbinary(max)").IsRequired();
                propertyOptions.PropertyDefault(x => x.CreatedDt).HasDefaultValueSql("GetUtcDate()");
                propertyOptions.PropertyDefault(x => x.Deleted).HasDefaultValue(false);
                propertyOptions.Property(x => x.DatabaseUrl).HasColumnType("nvarchar(200)");
                propertyOptions.Property(x => x.FileSystemUrl).HasColumnType("nvarchar(200)");
            });
        }
    }
}
