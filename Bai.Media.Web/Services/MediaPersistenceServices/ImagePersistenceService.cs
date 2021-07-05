using System;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Models;
using Bai.Media.Web.Services.MediaPersistenceServices.Base;

namespace Bai.Media.Web.Services.MediaPersistenceServices
{
    public class ImagePersistenceService : PersistenceService<Image, ImageEntity>
    {
        public ImagePersistenceService(IDomainRepository<ImageEntity, Guid> repository,
                                       IFormImageToEntityConverterService<Image, ImageEntity> baseImageService,
                                       IFileSystemService fileSystemService) : base(repository, baseImageService, fileSystemService)
        {
        }

        protected override string EntityName => "Image";

        protected override bool Where(ImageEntity entity, Guid modelKey) =>
            entity.PageId == modelKey;

        protected override Guid GetModelKey(Image model) =>
            model.PageId;

        protected override string GetEntityKeyAsString(ImageEntity entity) =>
            $"{entity.PageType}_{entity.PageId}".ToString();

        protected override void SetKeyFromModelToEntity(Image model, ImageEntity newMediaEntity) =>
            newMediaEntity.PageId = model.PageId;
    }
}
