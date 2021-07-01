using System;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Models;
using ImageMagick;
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
        public virtual async Task<ActionResult> Post([FromBody] Avatar entity)
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

            var userAvatar = await _repository.GetEntity(avatar => avatar.UserId == entity.UserId, asNoTracking: true);
            var newUserAvatar = _baseImageService.GetFileMetadata(entity);
            if (userAvatar == null)
            {
                await _repository.AddEntity(newUserAvatar, true);
                return Ok(newUserAvatar.Id);
            }

            userAvatar.Deleted = true;
            await _repository.UpdateEntity(userAvatar);
            await _repository.AddEntity(newUserAvatar, true);

            return Ok(new
            {
                DatabaseUrl = _baseImageService.GetDatabaseUrl(newUserAvatar, "avatar"),
                FileSystemUrl = _baseImageService.GetFileSystemUrl(newUserAvatar, "avatars")
            });
        }

        [HttpGet("{imageId}")]
        public async Task<ActionResult> GetAvatarFromDatabase(Guid imageId)
        {
            var userAvatar = await _repository.GetEntity(avatar => avatar.Id == imageId);
            return File(userAvatar.ImageBytes, userAvatar.ContentType);
        }
    }
}
