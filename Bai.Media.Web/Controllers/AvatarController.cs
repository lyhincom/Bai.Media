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

        public AvatarController(IDomainRepository<AvatarEntity, Guid> repository, IBaseImageService<Avatar, AvatarEntity> baseImageService)
        {
            _repository = repository;
            _baseImageService = baseImageService;
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([ModelBinder(typeof(AvatarBinder))] Avatar entity)
        {
            if (entity.FormImage.ContentType == "image/png" ||
                entity.FormImage.ContentType == "image/jpg" ||
                entity.FormImage.ContentType == "image/jpeg" ||
                entity.FormImage.ContentType == "image/gif")
            {
                throw new Exception("Allowed image ContentTypes: png, jpg, jpeg, gif.");
            }

            if (entity.FormImage.Length < 512)
            {
                throw new Exception("Image is too small to upload.");
            }

            if (entity.FormImage.Length > 300000)
            {
                throw new Exception("Image is too large to upload.");
            }

            var newUserAvatar = await AddOrUpdateUserAvatar(entity);
            var databaseUrl = _baseImageService.GetDatabaseUrl(newUserAvatar, "avatar");
            var fileSystemUrl = _baseImageService.GetFileSystemUrl(newUserAvatar, "avatars");

            return Ok(new MediaUrl
            {
                DatabaseUrl = databaseUrl,
                FileSystemUrl = fileSystemUrl
            });
        }

        [HttpGet("{imageId}")]
        public async Task<ActionResult> GetAvatarFromDatabase(Guid imageId)
        {
            var userAvatar = await _repository.GetEntity(avatar => avatar.Id == imageId);
            return File(userAvatar.ImageBytes, userAvatar.ContentType);
        }

        private async Task<AvatarEntity> AddOrUpdateUserAvatar(Avatar entity)
        {
            var userAvatar = await _repository.GetEntity(avatar => avatar.UserId == entity.UserId, asNoTracking: true);
            var newUserAvatar = _baseImageService.GetFileMetadata(entity);
            if (userAvatar == null)
            {
                newUserAvatar.UserId = entity.UserId;
                await _repository.AddEntity(newUserAvatar, true);
                return newUserAvatar;
            }

            userAvatar.Deleted = true;
            await _repository.UpdateEntity(userAvatar);

            newUserAvatar.UserId = entity.UserId;
            await _repository.AddEntity(newUserAvatar, true);
            
            return newUserAvatar;
        }
    }
}
