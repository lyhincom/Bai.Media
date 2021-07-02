using System;
using System.IO;
using Bai.Domain.Settings.Getters;
using Bai.General.API;
using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions.Models;
using Bai.Media.Web.Abstractions.Models;
using Bai.Media.Web.Abstractions.Services;
using ImageMagick;
using Microsoft.AspNetCore.StaticFiles;

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

            var fileExtension = Path.GetExtension(formImage.FileName);
            var contentType = GetMimeTypeFromFileName(formImage.FileName);
            return new TEntity
            {
                FileSizeInBytes = memoryStream.Length,
                FileExtension = fileExtension,
                ContentType = contentType,
                Height = image.Height,
                Width = image.Width,
                ImageBytes = memoryStream.ToArray()
            };
        }

        public string GetDatabaseUrl(TEntity entity, Guid keyId, string controllerName) =>
            DomainUrl.Combine(DomainUrls.Client, "api", controllerName, keyId.ToString()).ToLower();

        public string GetFileSystemUrl(TEntity entity, Guid keyId, string folderName) =>
            DomainUrl.Combine(DomainUrls.Client, "Bai.Media.StaticFiles", folderName, GetFileName(entity, keyId)).ToLower();

        protected abstract void ValidateImage(MagickImage image);

        private string GetMimeTypeFromFileName(string fileName)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);
            return contentType ?? "application/octet-stream";
        }

        private string GetFileName(TEntity entity, Guid keyId) =>
            $"{keyId}{entity.FileExtension}";
    }
}
