using System;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Abstractions;
using Bai.General.DAL.Abstractions.Models;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Abstractions.Models;
using Bai.Media.Web.Abstractions.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Abstractions.Services.PersistenceServices;
using Bai.Media.Web.Models;

namespace Bai.Media.Web.Services.MediaPersistenceServices.Base
{
    public abstract class PersistenceService<TModel, TEntity> : IPersistenceService<TModel> where TModel : IFormImage
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

        public async Task<MediaUrl> AddOrUpdateUserMedia(TModel model)
        {
            var newUserAvatar = await AddOrUpdateUserMediaDatabaseAndFileSystem(model);

            var modelKey = GetModelKey(model);
            var databaseUrl = MediaUrlService.GetDatabaseUrl(modelKey, EntityName);
            var fileSystemUrl = MediaUrlService.GetFileSystemUrl(newUserAvatar.FileExtension, modelKey, EntityName);
            await SaveImageUrlsToDatabase(newUserAvatar, databaseUrl, fileSystemUrl);

            return new MediaUrl
            {
                DatabaseUrl = databaseUrl,
                FileSystemUrl = fileSystemUrl
            };
        }

        private async Task<TEntity> AddOrUpdateUserMediaDatabaseAndFileSystem(TModel model)
        {
            var modelKey = GetModelKey(model);
            var entity = await _repository.GetEntity(entity => Where(entity, modelKey), asNoTracking: true);
            var newMediaEntity = _baseImageService.GetEntityFromFormImage(model);
            var entityGuidAndTypeId = GetEntityKeyAsString(entity);
            var fileName = $"{entityGuidAndTypeId}{_fileSystemService.GetFileExtension(model.FormImage)}";

            if (entity == null)
            {
                // Database
                SetKeyFromModelToEntity(model, newMediaEntity);
                await _repository.AddEntity(newMediaEntity, true);

                // FileSystem
                await _fileSystemService.AddFileToWwwRoot(model.FormImage, EntityName, fileName);

                return newMediaEntity;
            }

            // Database
            entity.Deleted = true;
            await _repository.UpdateEntity(entity);
            SetKeyFromModelToEntity(model, newMediaEntity);
            await _repository.AddEntity(newMediaEntity, true);

            // FileSystem
            _fileSystemService.ArchiveWwwRootFile(EntityName, fileName);
            await _fileSystemService.AddFileToWwwRoot(model.FormImage, EntityName, fileName);

            return newMediaEntity;
        }

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
