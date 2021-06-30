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
            builder.ToTable<Avatar>("Avatars", "dbo");
            builder.Entity<Avatar>().HasIndex(avatar => avatar.Id).IsUnique();
            builder.Entity<Avatar>(propertyOptions =>
            {
                propertyOptions.PropertyDefault(x => x.Id).HasDefaultValueSql("NEWID()");
                propertyOptions.PropertyDefault(x => x.UserId);
                propertyOptions.PropertySizeDefault(x => x.FileExtension, 10);
                propertyOptions.PropertyDefault(x => x.FileSize);
                propertyOptions.PropertyDefault(x => x.CreatedDt).HasDefaultValueSql("GetUtcDate()");
            });

            // dbo.Image
            builder.ToTable<Image>("Image", "dbo");
            builder.Entity<Image>().HasIndex(image => image.Id).IsUnique();
            builder.Entity<Image>(propertyOptions =>
            {
                propertyOptions.PropertyDefault(x => x.Id).HasDefaultValueSql("NEWID()");
                propertyOptions.PropertyDefault(x => x.UserId);
                propertyOptions.PropertySizeDefault(x => x.FileExtension, 10);
                propertyOptions.PropertyDefault(x => x.FileSize);
                propertyOptions.PropertyDefault(x => x.CreatedDt).HasDefaultValueSql("GetUtcDate()");
            });
        }
    }
}
