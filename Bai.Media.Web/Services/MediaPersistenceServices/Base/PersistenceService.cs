using System;
using System.Collections.Generic;
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
        private readonly IDomainRepository<TEntity, Guid> _repository;
        private readonly IFormImageToEntityConverterService<TModel, TEntity> _baseImageService;
        private readonly IFileSystemService _fileSystemService;

        protected abstract string EntityName { get; }

        public PersistenceService(IDomainRepository<TEntity, Guid> repository,
                                  IFormImageToEntityConverterService<TModel, TEntity> baseImageService,
                                  IFileSystemService fileSystemService)
        {
            _repository = repository;
            _baseImageService = baseImageService;
            _fileSystemService = fileSystemService;
        }

        public async Task<MediaUrl> AddOrUpdateUserMedia(TModel model, Expression<Func<TEntity, bool>> whereExpression, ImageSizeEnum[] imageSizes = null)
        {
            var newUserAvatar = await AddOrUpdateUserMediaDatabaseAndFileSystem(model, whereExpression, imageSizes);

            var modelKeyId = GetModelKeyId(model);
            var modelKeyString = GetModelKeyAsString(model);
            var databaseUrl = MediaUrlService.GetDatabaseUrl(modelKeyId, EntityName);

            var imageSize = (ImageSizeEnum?)(imageSizes == null ? null : ImageSizeEnum.Medium);
            var defaultFileName = MediaUrlService.GetFileName(modelKeyString, newUserAvatar.FileExtension, imageSize);
            var defaultFileSystemUrl = MediaUrlService.GetFileSystemUrl(EntityName, defaultFileName);

            await SaveImageUrlsToDatabase(newUserAvatar, databaseUrl, defaultFileSystemUrl);

            return new MediaUrl
            {
                DatabaseUrl = databaseUrl,
                FileSystemUrl = defaultFileSystemUrl
            };
        }

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

        private string[] GetFileNameArray(string modelKey, TModel model, IList<ImageSizeEnum> imageSizes = null)
        {
            if (imageSizes == null)
            {
                var fileExtension = _fileSystemService.GetFileExtension(model.FormImage);
                var fileName = MediaUrlService.GetFileName(modelKey, fileExtension);
                return new string[] { fileName };
            }

            return imageSizes.Select(imageSize =>
            {
                var fileExtension = _fileSystemService.GetFileExtension(model.FormImage);
                var result = MediaUrlService.GetFileName(modelKey, fileExtension, imageSize);

                return result;
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

        protected abstract Guid GetModelKeyId(TModel model);
        protected abstract string GetModelKeyAsString(TModel model);
        protected abstract void SetKeyFromModelToEntity(TModel model, TEntity newMediaEntity);

        private async Task SaveImageUrlsToDatabase(TEntity newUserAvatar, string databaseUrl, string fileSystemUrl)
        {
            newUserAvatar.DatabaseUrl = databaseUrl;
            newUserAvatar.FileSystemUrl = fileSystemUrl;
            await _repository.UpdateEntity(newUserAvatar, true);
        }
    }
}
