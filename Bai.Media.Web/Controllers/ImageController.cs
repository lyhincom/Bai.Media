using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Bai.Domain.Settings.Getters;
using Bai.Media.DAL.Contexts;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Abstractions.Services.PersistenceServices;
using Bai.Media.Web.Constants;
using Bai.Media.Web.Enums;
using Bai.Media.Web.Exceptions.Base;
using Bai.Media.Web.ModelBinders;
using Bai.Media.Web.Models;
using Bai.Media.Web.Services;
using Microsoft.AspNetCore.Mvc;
using DrawingImage = System.Drawing.Image;

namespace Bai.Media.Web.Controllers
{
    /// <summary>
    /// 1. Image API persists RawImage to database without Watermark, max Image quality;
    /// 2. All non-database images must have a Watermark;
    /// 3. Image API persists Thumbnail media to FileSystem (for SchoolImage page type) for Bai.Search;
    /// 4. Image API persists Medium media to FileSystem (for SchoolImage and ActivityImage page type) for Provider page and Activity page;
    /// 5. FileSystem images can be regenerated with a new Watermark from RawImage in database;
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IFormFileValidationService _formFileValidationService;
        private readonly IPersistenceService<Image, ImageEntity> _persistenceService;
        private readonly MediaDbContext _mediaDbContext;

        public ImageController(IFormFileValidationService formFileValidationService,
                               IPersistenceService<Image, ImageEntity> persistenceService,
                               MediaDbContext mediaDbContext)
        {
            _formFileValidationService = formFileValidationService;
            _persistenceService = persistenceService;
            _mediaDbContext = mediaDbContext;
        }

        [HttpGet("{pageId}/{imageType}/{imageSize}")]
        public ActionResult GetProcessedImageFromFileSystem(Guid pageId, string imageType, ImageSizeEnum imageSize = ImageSizeEnum.Thumbnail)
        {
            if (pageId == default)
            {
                return BadRequest($"{nameof(pageId)} cannot be default Guid");
            }

            if (!IsAllowedImageType(imageType))
            {
                return BadRequest($"{nameof(imageType)} should be: 'School' or 'Location'");
            }

            try
            {
                var imageUrl = $"{DomainUrls.Client}/bai.media.staticfiles/image/{pageId}_{imageType}_{ImageSizeTypes.GetImageSizePrefix(imageSize)}.jpg";
                var processedImageBytes = MediaService.DownloadImageFromUrlAsByteArray(imageUrl);
                return File(processedImageBytes, "image/jpeg");
            }
            catch (WebException)
            {
                return NotFound();
            }
        }

        [HttpGet("exists/{pageId}/{imageType}")]
        public ActionResult ImageExistsInDatabase(Guid pageId, string imageType)
        {
            if (pageId == default)
            {
                return BadRequest($"{nameof(pageId)} cannot be default Guid");
            }

            if (!IsAllowedImageType(imageType))
            {
                return BadRequest($"{nameof(imageType)} should be: 'School' or 'Location'");
            }

            var mediaExists = _mediaDbContext.Images.Any(img => img.PageId == pageId && img.PageType == imageType);

            return Ok(mediaExists);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([ModelBinder(typeof(ImageBinder))] Image model)
        {
            try
            {
                if (!IsAllowedImageType(model?.PageType))
                {
                    return BadRequest($"PageType {model.PageType} is not allowed");
                }

                _formFileValidationService.ValidateFormFile(model.FormImage);
                Expression<Func<ImageEntity, bool>> whereExpression = entity => entity.PageId == model.PageId && entity.PageType == model.PageType;
                var mediaUrl = await _persistenceService.AddOrUpdateMedia(model, whereExpression, new ImageSizeEnum[] { ImageSizeEnum.Thumbnail, ImageSizeEnum.Medium });

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

        private bool IsAllowedImageType(string imageType) =>
            imageType.ToLower() == ImageTypes.SchoolImage.ToLower() ||
            imageType.ToLower() == ImageTypes.LocationImage.ToLower();

        private bool IsStandardImageSize(DrawingImage image) =>
            image.Width != ImageValidationService.ConstWidth &&
            image.Height != ImageValidationService.ConstHeight;
    }
}
