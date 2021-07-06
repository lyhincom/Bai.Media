using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bai.Domain.Settings.Getters;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Abstractions.Services.PersistenceServices;
using Bai.Media.Web.Constants;
using Bai.Media.Web.Enums;
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
        private readonly IDomainRepository<ImageEntity, Guid> _repository;
        private readonly IFormFileValidationService _formFileValidationService;
        private readonly IMediaProcessingService _mediaProcessingService;
        private readonly IPersistenceService<Image, ImageEntity> _persistenceService;

        public ImageController(IDomainRepository<ImageEntity, Guid> repository,
                               IFormFileValidationService formFileValidationService,
                               IMediaProcessingService mediaProcessingService,
                               IPersistenceService<Image, ImageEntity> persistenceService)
        {
            _repository = repository;
            _formFileValidationService = formFileValidationService;
            _mediaProcessingService = mediaProcessingService;
            _persistenceService = persistenceService;
        }

        [HttpGet("{pageId}/{imageType}/{imageSize}")]
        public ActionResult GetProcessedImageFromFileSystem(Guid pageId, string imageType, ImageSizeEnum imageSize = ImageSizeEnum.Thumbnail)
        {
            if (pageId == default)
            {
                throw new ArgumentException($"{nameof(pageId)} cannot be default Guid");
            }

            if (imageType != ImageTypes.SchoolImage && imageType != ImageTypes.ActivityImage)
            {
                throw new ArgumentException($"{nameof(imageType)} should be: 'SchoolImage' or 'ActivityImage'");
            }

            var imageUrl = $"{DomainUrls.Client}/bai.media.staticfiles/image/{pageId}_{imageType}_{ImageSizeTypes.GetImageSizePrefix(imageSize)}.jpg";
            var processedImageBytes = MediaService.DownloadImageFromUrlAsByteArray(imageUrl);

            return File(processedImageBytes, "image/jpeg");
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([ModelBinder(typeof(ImageBinder))] Image model)
        {
            _formFileValidationService.ValidateFormFile(model.FormImage);
            Expression<Func<ImageEntity, bool>> whereExpression = entity => entity.PageId == model.PageId && entity.PageType == model.PageType;
            var mediaUrl = await _persistenceService.AddOrUpdateUserMedia(model, whereExpression, new ImageSizeEnum[] { ImageSizeEnum.Thumbnail, ImageSizeEnum.Medium });

            return Ok(mediaUrl);
        }

        #region Debug

        [HttpDelete]
        public virtual async Task<ActionResult> Delete(Guid pageId, string pageType)
        {
            throw new NotImplementedException();
        }

#endregion

        private bool IsStandardImageSize(DrawingImage image) =>
            image.Width != ImageValidationService.ConstWidth &&
            image.Height != ImageValidationService.ConstHeight;
    }
}
