using System;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Abstractions.Services.PersistenceServices;
using Bai.Media.Web.Exceptions.Base;
using Bai.Media.Web.ModelBinders;
using Bai.Media.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai.Media.Web.Controllers
{
    /// <summary>
    /// Avatar media has 1:1 proportion
    /// Avatar media does not have a Watermark
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AvatarController : ControllerBase
    {
        private readonly IDomainRepository<AvatarEntity, Guid> _repository;
        private readonly IFormFileValidationService _formFileValidationService;
        private readonly IPersistenceService<Avatar, AvatarEntity> _persistenceService;

        public AvatarController(IDomainRepository<AvatarEntity, Guid> repository,
                                IFormFileValidationService formFileValidationService,
                                IPersistenceService<Avatar, AvatarEntity> persistenceService)
        {
            _repository = repository;
            _formFileValidationService = formFileValidationService;
            _persistenceService = persistenceService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAvatarFromDatabase(Guid userId)
        {
            var userAvatar = await _repository.GetEntity(avatar => avatar.UserId == userId && avatar.Deleted == false);
            if (userAvatar == null)
            {
                return NotFound();
            }

            return File(userAvatar.ImageBytes, userAvatar.ContentType);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([ModelBinder(typeof(AvatarBinder))] Avatar model)
        {
            try
            {
                _formFileValidationService.ValidateFormFile(model.FormImage);
                var mediaUrl = await _persistenceService.AddOrUpdateMedia(model, entity => entity.UserId == model.UserId);

                return Ok(mediaUrl);
            }
            catch (MediaValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        #region Debug

        [HttpDelete]
        public virtual async Task<ActionResult> Delete(Guid userId)
        {
            await _persistenceService.DeleteMedia(userId);
            return Ok();
        }
            
        #endregion
    }
}
