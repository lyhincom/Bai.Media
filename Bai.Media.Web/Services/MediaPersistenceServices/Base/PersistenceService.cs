using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
    public abstract class PersistenceService<TModel, TEntity> : IPersistenceService<TModel, TEntity>
        where TModel : IFormImage
        where TEntity : GuidEntity, IImage, IImageUrls, ISoftDelete, new()
    {
        protected readonly IDomainRepository<TEntity, Guid> _repository;
        protected readonly IFileSystemService _fileSystemService;
        private readonly IFormImageToEntityConverterService<TModel, TEntity> _baseImageService;

        protected abstract string EntityName { get; }

        public PersistenceService(IDomainRepository<TEntity, Guid> repository,
                                  IFormImageToEntityConverterService<TModel, TEntity> baseImageService,
                                  IFileSystemService fileSystemService)
        {
            _repository = repository;
            _baseImageService = baseImageService;
            _fileSystemService = fileSystemService;
        }

        public async Task<MediaUrl> AddOrUpdateMedia(TModel model, Expression<Func<TEntity, bool>> whereExpression, ImageSizeEnum[] imageSizes = null)
        {
            var newMedia = await AddOrUpdateUserMediaDatabaseAndFileSystem(model, whereExpression, imageSizes);

            var modelKeyId = GetModelKeyId(model);
            var modelKeyString = GetModelKeyAsString(model);
            var databaseUrl = MediaUrlService.GetDatabaseUrl(modelKeyId, EntityName);

            var imageSize = (ImageSizeEnum?)(imageSizes == null ? (ImageSizeEnum?)null : ImageSizeEnum.Medium);
            var defaultFileName = MediaUrlService.GetFileName(modelKeyString, newMedia.FileExtension, imageSize);
            var defaultFileSystemUrl = MediaUrlService.GetFileSystemUrl(EntityName, defaultFileName);

            await SaveImageUrlsToDatabase(newMedia, databaseUrl, defaultFileSystemUrl);

            var hasDatabaseUrl = imageSizes == null;
            return new MediaUrl
            {
                DatabaseUrl = hasDatabaseUrl ? databaseUrl : null,
                FileSystemUrl = defaultFileSystemUrl
            };
        }

        public abstract Task DeleteMedia(Guid keyId);

        protected async Task DeleteMedia(Guid keyId, Expression<Func<TEntity, bool>> filterExpression)
        {
            _fileSystemService.DeleteWwwRootMedia(keyId, EntityName);

            var mediaArray = await _repository.GetEntities(filterExpression, asNoTracking: true);
            mediaArray.ToList().ForEach(logo => _repository.DeleteEntity(logo.Id));
            await _repository.Save();
        }

        protected abstract Guid GetModelKeyId(TModel model);
        protected abstract string GetModelKeyAsString(TModel model);
        protected abstract void SetKeyFromModelToEntity(TModel model, TEntity newMediaEntity);

        private async Task<TEntity> AddOrUpdateUserMediaDatabaseAndFileSystem(TModel model, Expression<Func<TEntity, bool>> whereExpression, IList<ImageSizeEnum> imageSizes = null)
        {
            var modelKeyString = GetModelKeyAsString(model);
            var entity = await _repository.GetEntity(whereExpression, asNoTracking: true);
            var newMediaEntity = _baseImageService.GetEntityFromFormImageAndValidate(model);
            
            var fileNames = GetFileNameArray(modelKeyString, model, imageSizes);

            newMediaEntity = await PersistToDatabase(model, entity, newMediaEntity);
            await PersistToFileSystem(model, entity, fileNames);

            return newMediaEntity;
        }

        private MediaFileSystem[] GetFileNameArray(string modelKey, TModel model, IList<ImageSizeEnum> imageSizes = null)
        {
            if (imageSizes == null)
            {
                var fileExtension = _fileSystemService.GetFileExtension(model.FormImage);
                var fileName = MediaUrlService.GetFileName(modelKey, fileExtension);
                return new MediaFileSystem[] { new MediaFileSystem(fileName) };
            }

            return imageSizes.Select(imageSize =>
            {
                var fileExtension = _fileSystemService.GetFileExtension(model.FormImage);
                var fileName = MediaUrlService.GetFileName(modelKey, fileExtension, imageSize);

                return new MediaFileSystem(fileName, imageSize);
            }).ToArray();
        }

        private async Task<TEntity> PersistToDatabase(TModel model, TEntity entity, TEntity newMediaEntity)
        {
            if (entity == null)
            {
                SetKeyFromModelToEntity(model, newMediaEntity);
                await _repository.AddEntity(newMediaEntity, true);

                return newMediaEntity;
            }

            await _repository.DeleteEntity(entity.Id);
            SetKeyFromModelToEntity(model, newMediaEntity);
            await _repository.AddEntity(newMediaEntity, true);

            return newMediaEntity;
        }

        private async Task PersistToFileSystem(TModel model, TEntity entity, MediaFileSystem[] mediaArray)
        {
            if (entity == null)
            {
                await _fileSystemService.AddFileToWwwRoot(model.FormImage, EntityName, mediaArray);
                return;
            }

            var fileNames = mediaArray.Select(media => Path.GetFileNameWithoutExtension(media.MediaPath)).ToArray();
            _fileSystemService.ArchiveWwwRootFile(EntityName, fileNames, entity.FileExtension);

            await _fileSystemService.AddFileToWwwRoot(model.FormImage, EntityName, mediaArray);
        }

        private async Task SaveImageUrlsToDatabase(TEntity newUserAvatar, string databaseUrl, string fileSystemUrl)
        {
            newUserAvatar.DatabaseUrl = databaseUrl;
            newUserAvatar.FileSystemUrl = fileSystemUrl;
            await _repository.UpdateEntity(newUserAvatar, true);
        }
    }
}
