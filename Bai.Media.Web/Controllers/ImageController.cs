using System;
using System.Net;
using System.Threading.Tasks;
using Bai.Domain.Settings.Getters;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Abstractions.Services.PersistenceServices;
using Bai.Media.Web.Enums;
using Bai.Media.Web.ModelBinders;
using Bai.Media.Web.Models;
using Bai.Media.Web.Services;
using Microsoft.AspNetCore.Mvc;
using DrawingImage = System.Drawing.Image;

namespace Bai.Media.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IDomainRepository<ImageEntity, Guid> _repository;
        private readonly IFormFileValidationService _formFileValidationService;
        private readonly IMediaProcessingService _mediaProcessingService;
        private readonly IPersistenceService<Image> _persistenceService;

        public ImageController(IDomainRepository<ImageEntity, Guid> repository,
                               IFormFileValidationService formFileValidationService,
                               IMediaProcessingService mediaProcessingService,
                               IPersistenceService<Image> persistenceService)
        {
            _repository = repository;
            _formFileValidationService = formFileValidationService;
            _mediaProcessingService = mediaProcessingService;
            _persistenceService = persistenceService;
        }

        [HttpGet]
        public ActionResult GetProcessedImage(Guid pageId, string pageType, ImageSizeEnum imageSize = ImageSizeEnum.Thumbnail)
        {
            using var webClient = new WebClient();
            string imageUrl = $"{DomainUrls.Client}/bai.media.staticfiles/predefined/images/calendar-application.jpg"; // ToDo: read Watermarked Image from FileSystem
            string watermarkUrl = $"{DomainUrls.Client}/bai.media.staticfiles/predefined/watermarks/default.png";

            var imageBytes = webClient.DownloadData(imageUrl);
            var watermarkBytes = webClient.DownloadData(watermarkUrl);

            using var image = MediaService.GetImageFromByteArray(imageBytes);
            using var watermark = MediaService.GetImageFromByteArray(watermarkBytes);

            DrawingImage resizedImage = null;
            if (imageSize == ImageSizeEnum.Thumbnail && IsStandardImageSize(image))
            {
                resizedImage = _mediaProcessingService.ResizeImage(image);
            }

            var processedImage = _mediaProcessingService.AddWatermarkSystemDrawing(resizedImage, watermark);
            var processedImageBytes = MediaService.ImageToByteArray(processedImage);

            return File(processedImageBytes, "image/jpeg");
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([ModelBinder(typeof(ImageBinder))] Image model)
        {
            _formFileValidationService.ValidateFormFile(model.FormImage);
            var mediaUrl = await _persistenceService.AddOrUpdateUserMedia(model);

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
