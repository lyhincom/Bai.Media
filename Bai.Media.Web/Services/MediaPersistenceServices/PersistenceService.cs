using System;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Abstractions.Services.PersistenceServices;
using Bai.Media.Web.Models;

namespace Bai.Media.Web.Services.MediaPersistenceServices
{
    public class PersistenceService : IPersistenceService<Avatar>
    {
        private readonly IDomainRepository<AvatarEntity, Guid> _repository;
        private readonly IFormImageToEntityConverterService<Avatar, AvatarEntity> _baseImageService;
        private readonly IFileSystemService _fileSystemService;

        public PersistenceService(IDomainRepository<AvatarEntity, Guid> repository,
                                  IFormImageToEntityConverterService<Avatar, AvatarEntity> baseImageService,
                                  IFileSystemService fileSystemService)
        {
            _repository = repository;
            _baseImageService = baseImageService;
            _fileSystemService = fileSystemService;
        }

        public async Task<MediaUrl> AddOrUpdateUserAvatar(Avatar model)
        {
            var newUserAvatar = await AddOrUpdateUserAvatarDatabaseAndFileSystem(model);

            var databaseUrl = MediaUrlService.GetDatabaseUrl(model.UserId, "avatar");
            var fileSystemUrl = MediaUrlService.GetFileSystemUrl(newUserAvatar.FileExtension, model.UserId, "avatars");
            await SaveImageUrlsToDatabase(newUserAvatar, databaseUrl, fileSystemUrl);

            return new MediaUrl
            {
                DatabaseUrl = databaseUrl,
                FileSystemUrl = fileSystemUrl
            };
        }

        private async Task<AvatarEntity> AddOrUpdateUserAvatarDatabaseAndFileSystem(Avatar model)
        {
            var entity = await _repository.GetEntity(avatar => avatar.UserId == model.UserId, asNoTracking: true);
            var newUserAvatar = _baseImageService.GetEntityFromFormImage(model);
            var fileName = $"{model.UserId}{_fileSystemService.GetFileExtension(model.FormImage)}";

            if (entity == null)
            {
                // Database
                newUserAvatar.UserId = model.UserId;
                await _repository.AddEntity(newUserAvatar, true);

                // FileSystem
                await _fileSystemService.AddFileToWwwRoot(model.FormImage, "Avatars", fileName);

                return newUserAvatar;
            }

            // Database
            entity.Deleted = true;
            await _repository.UpdateEntity(entity);
            newUserAvatar.UserId = model.UserId;
            await _repository.AddEntity(newUserAvatar, true);


            // FileSystem
            _fileSystemService.ArchiveWwwRootFile("Avatars", fileName);
            await _fileSystemService.AddFileToWwwRoot(model.FormImage, "Avatars", fileName);

            return newUserAvatar;
        }

        private async Task SaveImageUrlsToDatabase(AvatarEntity newUserAvatar, string databaseUrl, string fileSystemUrl)
        {
            newUserAvatar.DatabaseUrl = databaseUrl;
            newUserAvatar.FileSystemUrl = fileSystemUrl;
            await _repository.UpdateEntity(newUserAvatar, true);
        }
    }
}
