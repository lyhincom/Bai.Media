using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Abstractions;
using Bai.General.DAL.Abstractions.Models;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Abstractions.Models;
using Bai.Media.Web.Abstractions.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Abstractions.Services.PersistenceServices;
using Bai.Media.Web.Enums;
using Bai.Media.Web.Models;

namespace Bai.Media.Web.Services.MediaPersistenceServices.Base
{
    public abstract class PersistenceService<TModel, TEntity> : IPersistenceService<TModel>
        where TModel : IFormImage
        where TEntity : GuidEntity, IImage, IImageUrls, ISoftDelete, new()
    {
        private readonly IDomainRepository<TEntity, Guid> _repository;
        private readonly IFormImageToEntityConverterService<TModel, TEntity> _baseImageService;
        private readonly IFileSystemService _fileSystemService;

        protected abstract string EntityName { get; }
        protected abstract bool Where(TEntity entity, Guid modelKey);

        public PersistenceService(IDomainRepository<TEntity, Guid> repository,
                                  IFormImageToEntityConverterService<TModel, TEntity> baseImageService,
                                  IFileSystemService fileSystemService)
        {
            _repository = repository;
            _baseImageService = baseImageService;
            _fileSystemService = fileSystemService;
        }

        public async Task<MediaUrl> AddOrUpdateUserMedia(TModel model, ImageSizeEnum[] imageSizes = null)
        {
            var newUserAvatar = await AddOrUpdateUserMediaDatabaseAndFileSystem(model, imageSizes);

            var modelKey = GetModelKey(model);
            var databaseUrl = MediaUrlService.GetDatabaseUrl(modelKey, EntityName);

            var imageSize = (ImageSizeEnum?)(imageSizes == null ? null : ImageSizeEnum.Medium);
            var defaultFileName = GetFileName(modelKey, newUserAvatar.FileExtension, imageSize);
            var defaultFileSystemUrl = MediaUrlService.GetFileSystemUrl(EntityName, defaultFileName);

            await SaveImageUrlsToDatabase(newUserAvatar, databaseUrl, defaultFileSystemUrl);

            return new MediaUrl
            {
                DatabaseUrl = databaseUrl,
                FileSystemUrl = defaultFileSystemUrl
            };
        }

        private async Task<TEntity> AddOrUpdateUserMediaDatabaseAndFileSystem(TModel model, IList<ImageSizeEnum> imageSizes = null)
        {
            var modelKey = GetModelKey(model);
            var entity = await _repository.GetEntity(entity => Where(entity, modelKey), asNoTracking: true);
            var newMediaEntity = _baseImageService.GetEntityFromFormImage(model);
            var entityGuidAndTypeId = GetEntityKeyAsString(entity);
            
            var fileNames = (string[])(imageSizes == null ?
                new List<string> { GetFileName(modelKey, _fileSystemService.GetFileExtension(model.FormImage)) } :
                imageSizes.Select(imageSize => GetFileName(modelKey, _fileSystemService.GetFileExtension(model.FormImage), imageSize)));

            newMediaEntity = await PersistToDatabase(model, entity, newMediaEntity);
            await PersistToFileSystem(model, entity, fileNames);

            return newMediaEntity;
        }

        private async Task<TEntity> PersistToDatabase(TModel model, TEntity entity, TEntity newMediaEntity)
        {
            if (entity == null)
            {
                SetKeyFromModelToEntity(model, newMediaEntity);
                await _repository.AddEntity(newMediaEntity, true);

                return newMediaEntity;
            }

            entity.Deleted = true;
            await _repository.UpdateEntity(entity);
            SetKeyFromModelToEntity(model, newMediaEntity);
            await _repository.AddEntity(newMediaEntity, true);

            return newMediaEntity;
        }

        private async Task PersistToFileSystem(TModel model, TEntity entity, string[] fileNames)
        {
            if (entity == null)
            {
                await _fileSystemService.AddFileToWwwRoot(model.FormImage, EntityName, fileNames);
                return;
            }

            _fileSystemService.ArchiveWwwRootFile(EntityName, fileNames);
            await _fileSystemService.AddFileToWwwRoot(model.FormImage, EntityName, fileNames);
        }

        private string GetFileName(Guid modelKey, string fileExtension, ImageSizeEnum? imageSize = null) =>
            imageSize.HasValue ?
                MediaUrlService.GetFileName(modelKey, fileExtension, imageSize.Value) :
                MediaUrlService.GetFileName(modelKey, fileExtension);

        protected abstract Guid GetModelKey(TModel model);
        protected abstract string GetEntityKeyAsString(TEntity entity);
        protected abstract void SetKeyFromModelToEntity(TModel model, TEntity newMediaEntity);

        private async Task SaveImageUrlsToDatabase(TEntity newUserAvatar, string databaseUrl, string fileSystemUrl)
        {
            newUserAvatar.DatabaseUrl = databaseUrl;
            newUserAvatar.FileSystemUrl = fileSystemUrl;
            await _repository.UpdateEntity(newUserAvatar, true);
        }
    }
}
