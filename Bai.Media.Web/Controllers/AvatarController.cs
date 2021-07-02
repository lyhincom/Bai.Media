using System;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.ModelBinders;
using Bai.Media.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai.Media.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvatarController : ControllerBase
    {
        private readonly IDomainRepository<AvatarEntity, Guid> _repository;
        private readonly IBaseImageService<Avatar, AvatarEntity> _baseImageService;
        private readonly IFileSystemService _fileSystemService;

        public AvatarController(IDomainRepository<AvatarEntity, Guid> repository,
                                IBaseImageService<Avatar, AvatarEntity> baseImageService,
                                IFileSystemService fileSystemService)
        {
            _repository = repository;
            _baseImageService = baseImageService;
            _fileSystemService = fileSystemService;
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([ModelBinder(typeof(AvatarBinder))] Avatar model)
        {
            if (model.FormImage.ContentType == "image/png" ||
                model.FormImage.ContentType == "image/jpg" ||
                model.FormImage.ContentType == "image/jpeg" ||
                model.FormImage.ContentType == "image/gif")
            {
                throw new Exception("Allowed image ContentTypes: png, jpg, jpeg, gif.");
            }

            if (model.FormImage.Length < 512)
            {
                throw new Exception("Image is too small to upload.");
            }

            if (model.FormImage.Length > 300000)
            {
                throw new Exception("Image is too large to upload.");
            }

            var newUserAvatar = await AddOrUpdateUserAvatar(model);
            var databaseUrl = _baseImageService.GetDatabaseUrl(newUserAvatar, model.UserId, "avatar");
            var fileSystemUrl = _baseImageService.GetFileSystemUrl(newUserAvatar, model.UserId, "avatars");

            await SaveImageUrlsToDatabase(newUserAvatar, databaseUrl, fileSystemUrl);

            return Ok(new MediaUrl
            {
                DatabaseUrl = databaseUrl,
                FileSystemUrl = fileSystemUrl
            });
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAvatarFromDatabase(Guid userId)
        {
            var userAvatar = await _repository.GetEntity(avatar => avatar.UserId == userId && avatar.Deleted == false);
            if (userAvatar == null)
            {
                return Ok(string.Empty);
            }

            return File(userAvatar.ImageBytes, userAvatar.ContentType);
        }

        private async Task<AvatarEntity> AddOrUpdateUserAvatar(Avatar model)
        {
            var entity = await _repository.GetEntity(avatar => avatar.UserId == model.UserId, asNoTracking: true);
            var newUserAvatar = _baseImageService.GetFileMetadata(model);
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
            try
            {
                entity.Deleted = true;
                await _repository.UpdateEntity(entity);
                newUserAvatar.UserId = model.UserId;
                await _repository.AddEntity(newUserAvatar, true);
            }
            catch (Exception e)
            {
                var x = e.Message;
            }

            // FileSystem
            await _fileSystemService.ArchiveWwwRootFile("Avatars", fileName);
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
