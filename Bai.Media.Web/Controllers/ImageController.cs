using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Services.MediaServices;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;

namespace Bai.Media.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IDomainRepository<ImageEntity, Guid> _repository;

        public ImageController(IDomainRepository<ImageEntity, Guid> repository) =>
            _repository = repository;

        //[HttpPost]
        //public virtual async Task<ActionResult> Post([FromBody] TModel entity) =>
        //    Ok(await _repository.AddEntity(entity, true));

        [HttpGet]
        public async Task<ActionResult> WatermarkImage() => GetWatermarkImage();

        private ActionResult WatermarkMagicImage()
        {
            using var webClient = new WebClient();
            string imageUrl = "https://localhost:13001/bai.media.staticfiles/predefined/images/default.jpg"; // default.jpg // calendar-application.jpg
            string watermarkUrl = "https://localhost:13001/bai.media.staticfiles/predefined/watermarks/default.png";

            var imageBytes = webClient.DownloadData(imageUrl);
            var watermarkBytes = webClient.DownloadData(watermarkUrl);

            var imageStream = GetStreamFromByteArray(imageBytes);
            var watermarkStream = GetStreamFromByteArray(watermarkBytes);

            var processedImage = new WatermarkService().AddWatermark(imageStream, watermarkStream);

            return File(processedImage.ToByteArray(), "image/jpeg");
        }

        private ActionResult GetWatermarkImage(bool defaultSize = true)
        {
            using var webClient = new WebClient();
            string imageUrl = "https://localhost:13001/bai.media.staticfiles/predefined/images/calendar-application.jpg";
            string watermarkUrl = "https://localhost:13001/bai.media.staticfiles/predefined/watermarks/default.png";

            var imageBytes = webClient.DownloadData(imageUrl);
            var watermarkBytes = webClient.DownloadData(watermarkUrl);

            var image = GetImageFromByteArray(imageBytes);
            using var watermark = GetImageFromByteArray(watermarkBytes);

            if (defaultSize)
            {
                image = new WatermarkService().ResizeImage(image);
            }

            var processedImage = new WatermarkService().AddWatermark2(image, watermark);
            var processedImageBytes = ImageToByteArray(processedImage);
            return File(processedImageBytes, "image/jpeg");
        }

        //private string GetMimeTypeFromFileName(string fileName)
        //{
        //    new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);
        //    return contentType ?? "application/octet-stream";
        //}

        private Stream GetStreamFromByteArray(byte[] byteArray)
        {
            var ms = new MemoryStream(byteArray, 0, byteArray.Length);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        private Image GetImageFromByteArray(byte[] byteArray)
        {
            var ms = new MemoryStream(byteArray, 0, byteArray.Length);
            ms.Seek(0, SeekOrigin.Begin);
            return Image.FromStream(ms);
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            try
            {
                var converter = new ImageConverter();
                return (byte[])converter.ConvertTo(imageIn, typeof(byte[]));

                using (var ms = new MemoryStream())
                {
                    imageIn.Save(ms, ImageFormat.Png); // , imageIn.RawFormat
                    return ms.ToArray();
                }
            }
            catch (Exception e)
            {
                var x = e;
                return null;
            }
        }
    }
}
