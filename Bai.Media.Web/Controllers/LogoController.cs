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
    /// Logo media has rectangle or square 1:1 proportion
    /// Logo media does not have a Watermark
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LogoController : ControllerBase
    {
        private readonly IDomainRepository<LogoEntity, Guid> _repository;
        private readonly IFormFileValidationService _formFileValidationService;
        private readonly IPersistenceService<Logo, LogoEntity> _persistenceService;
        private readonly IFileSystemService _fileSystemService;

        public LogoController(IDomainRepository<LogoEntity, Guid> repository,
                              IFormFileValidationService formFileValidationService,
                              IPersistenceService<Logo, LogoEntity> persistenceService,
                              IFileSystemService fileSystemService)
        {
            _repository = repository;
            _formFileValidationService = formFileValidationService;
            _persistenceService = persistenceService;
            _fileSystemService = fileSystemService;
        }

        [HttpGet("{pageId}")]
        public async Task<ActionResult> GetLogoFromDatabase(Guid pageId)
        {
            var userLogo = await _repository.GetEntity(logo => logo.PageId == pageId && logo.Deleted == false);
            if (userLogo == null)
            {
                return Ok(string.Empty);
            }

            return File(userLogo.ImageBytes, userLogo.ContentType);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([ModelBinder(typeof(LogoBinder))] Logo model)
        {
            try
            {
                _formFileValidationService.ValidateFormFile(model.FormImage);
                var mediaUrl = await _persistenceService.AddOrUpdateMedia(model, entity => entity.PageId == model.PageId);

                return Ok(mediaUrl);
            }
            catch (MediaValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        #region Debug

        [HttpDelete]
        public virtual async Task<ActionResult> Delete(Guid pageId)
        {
            await _persistenceService.DeleteMedia(pageId);
            return Ok();
        }

        #endregion
    }
}
