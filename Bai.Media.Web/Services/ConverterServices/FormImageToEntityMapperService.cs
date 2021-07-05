using System.IO;
using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions.Models;
using Bai.Media.Web.Abstractions.Models;
using Bai.Media.Web.Abstractions.Services;
using ImageMagick;
using Microsoft.AspNetCore.StaticFiles;

namespace Bai.Media.Web.Services.ConverterServices
{
    public abstract class FormImageToEntityMapperService<TModel, TEntity> : IFormImageToEntityConverterService<TModel, TEntity>
        where TModel : IFormImage
        where TEntity : GuidEntity, IImage, new()
    {
        private readonly IMagicImageValidationService<TModel, TEntity> _mediaValidationService;

        public FormImageToEntityMapperService(IMagicImageValidationService<TModel, TEntity> mediaValidationService) =>
            _mediaValidationService = mediaValidationService;

        public TEntity GetEntityFromFormImage(IFormImage model)
        {
            var formImage = model.FormImage;
            using var memoryStream = new MemoryStream();
            formImage.CopyTo(memoryStream);

            using var image = new MagickImage(memoryStream.ToArray());
            _mediaValidationService.ValidateImage(image);

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

        private string GetMimeTypeFromFileName(string fileName)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);
            return contentType ?? "application/octet-stream";
        }
    }
}
