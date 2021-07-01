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

        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // dbo.Avatar
            builder.Entity<Avatar>(indexOptions =>
            {
                indexOptions.ToTable("Avatars", "dbo");
                indexOptions.HasKey(avatar => avatar.Id)
                            .HasName("PK_Avatar_Id");

                indexOptions.HasIndex(avatar => new { avatar.UserId })
                            .HasDatabaseName("IX_Avatar_Key");
                indexOptions.HasIndex(avatar => new { avatar.UserId,
                                                      avatar.FileExtension, avatar.FileSize, avatar.CreatedDt, avatar.Deleted })
                            .HasDatabaseName("IX_Avatar_QueryFields");
            });
            builder.Entity<Avatar>(propertyOptions =>
            {
                propertyOptions.PropertyDefault(x => x.Id).HasDefaultValueSql("NEWID()");
                
                propertyOptions.PropertyDefault(x => x.UserId);
                
                propertyOptions.PropertySizeDefault(x => x.FileExtension, 10);
                propertyOptions.PropertyDefault(x => x.FileSize);
                propertyOptions.Property(x => x.ImageBytes).HasColumnType("varbinary(max)").IsRequired();
                propertyOptions.PropertyDefault(x => x.CreatedDt).HasDefaultValueSql("GetUtcDate()");
                propertyOptions.PropertyDefault(x => x.Deleted).HasDefaultValue(false);
            });

            // dbo.Image
            builder.Entity<Image>(indexOptions =>
            {
                indexOptions.ToTable("Image", "dbo");
                indexOptions.HasKey(image => image.Id)
                            .HasName("PK_Image_Id");

                indexOptions.HasIndex(image => new { image.UserId, image.PageId, image.PageType, image.ImageGroupId })
                            .HasDatabaseName("IX_Image_Key");
                indexOptions.HasIndex(image => new { image.UserId, image.PageId, image.PageType, image.ImageGroupId,
                                                     image.FileExtension, image.FileSize, image.CreatedDt, image.Deleted })
                            .HasDatabaseName("IX_Image_QueryFields");
            });
            builder.Entity<Image>(propertyOptions =>
            {
                propertyOptions.PropertyDefault(x => x.Id).HasDefaultValueSql("NEWID()");
                
                propertyOptions.PropertyDefault(x => x.UserId);
                propertyOptions.PropertyDefault(x => x.PageId);
                propertyOptions.PropertySizeDefault(x => x.PageType, 100);
                propertyOptions.Property(x => x.ImageGroupId).HasColumnType("uniqueidentifier");

                propertyOptions.PropertySizeDefault(x => x.FileExtension, 10);
                propertyOptions.PropertyDefault(x => x.FileSize);
                propertyOptions.Property(x => x.ImageBytes).HasColumnType("varbinary(max)").IsRequired();
                propertyOptions.PropertyDefault(x => x.CreatedDt).HasDefaultValueSql("GetUtcDate()");
                propertyOptions.PropertyDefault(x => x.Deleted).HasDefaultValue(false);
                
            });

            // dbo.Logo
            builder.Entity<Logo>(indexOptions =>
            {
                indexOptions.ToTable("Logo", "dbo");
                indexOptions.HasKey(logo => logo.Id)
                            .HasName("PK_Logo_Id");

                indexOptions.HasIndex(logo => new { logo.PageId })
                            .HasDatabaseName("IX_Logo_Key");
                indexOptions.HasIndex(logo => new { logo.PageId,
                                                    logo.FileExtension, logo.FileSize, logo.CreatedDt, logo.Deleted })
                            .HasDatabaseName("IX_Logo_QueryFields");
            });
            builder.Entity<Logo>(propertyOptions =>
            {
                propertyOptions.PropertyDefault(x => x.Id).HasDefaultValueSql("NEWID()");
                
                propertyOptions.PropertyDefault(x => x.PageId);

                propertyOptions.PropertySizeDefault(x => x.FileExtension, 10);
                propertyOptions.PropertyDefault(x => x.FileSize);
                propertyOptions.Property(x => x.ImageBytes).HasColumnType("varbinary(max)").IsRequired();
                propertyOptions.PropertyDefault(x => x.CreatedDt).HasDefaultValueSql("GetUtcDate()");
                propertyOptions.PropertyDefault(x => x.Deleted).HasDefaultValue(false);
            });
        }
    }
}
