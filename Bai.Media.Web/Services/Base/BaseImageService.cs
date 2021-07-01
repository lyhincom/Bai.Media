using System.IO;
using System.Threading.Tasks;
using Bai.Domain.Settings.Getters;
using Bai.General.API;
using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions.Models;
using Bai.Media.Web.Abstractions.Models;
using Bai.Media.Web.Abstractions.Services;
using ImageMagick;

namespace Bai.Media.Web.Services.Base
{
    public abstract class BaseImageService<TModel, TEntity> : IBaseImageService<TModel, TEntity>
        where TModel : IFormImage
        where TEntity : GuidEntity, IImage, new()
    {
        public TEntity GetFileMetadata(IFormImage model)
        {
            var formImage = model.FormImage;
            using var memoryStream = new MemoryStream();
            formImage.CopyTo(memoryStream);

            using var image = new MagickImage(memoryStream.ToArray());
            ValidateImage(image);

            return new TEntity
            {
                FileSizeInBytes = memoryStream.Length,
                FileExtension = Path.GetExtension(formImage.FileName),
                ContentType = formImage.ContentType,
                Height = image.Height,
                Width = image.Width,
                ImageBytes = memoryStream.ToArray()
            };
        }

        public abstract void ValidateImage(MagickImage image);

        public async Task SaveToFileSystem(TModel model, TEntity entity)
        {
            var filePath = GetWwwRootPath(entity);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await model.FormImage.CopyToAsync(fileStream);
        }

        public string GetDatabaseUrl(TEntity entity, string controllerName) =>
            DomainUrl.Combine(DomainUrls.Media, "api", controllerName, entity.Id.ToString());

        public string GetFileSystemUrl(TEntity entity, string folderName) =>
            DomainUrl.Combine(DomainUrls.Media, "Bai.Media.StaticFiles", folderName, GetFileName(entity));

        private string GetWwwRootPath(TEntity entity) =>
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", GetFileName(entity));

        private string GetFileName(TEntity entity) =>
            $"{entity.Id}{entity.FileExtension}";
    }
}
