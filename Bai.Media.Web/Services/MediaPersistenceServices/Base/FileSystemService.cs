using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bai.Domain.Settings.Getters;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Constants;
using Bai.Media.Web.Enums;
using Bai.Media.Web.Extensions;
using Bai.Media.Web.Models;
using Microsoft.AspNetCore.Http;
using DrawingImage = System.Drawing.Image;

namespace Bai.Media.Web.Services.MediaPersistenceServices.Base
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IMediaProcessingService _mediaProcessingService;

        public FileSystemService(IMediaProcessingService mediaProcessingService) =>
            _mediaProcessingService = mediaProcessingService;

        public async Task AddFileToWwwRoot(IFormFile formFile, string wwwRootDirectoryPath, params MediaFileSystem[] mediaArray)
        {
            foreach(var media in mediaArray)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", wwwRootDirectoryPath, media.MediaPath);

                IFormFile processedFormFile = null;
                if (media.MediaSize != null)
                {
                    processedFormFile = await ProcessImage(formFile, media.MediaSize.Value);
                }

                await PersistFileToPath(processedFormFile ?? formFile, filePath);
            }
        }

        public void ArchiveWwwRootFile(string wwwRootDirectoryPath, params string[] fileNames)
        {
            foreach (var fileName in fileNames)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", wwwRootDirectoryPath, fileName);
                ArchiveFile(filePath);
            }
        }

        public void DeleteWwwRootMedia(Guid keyId, string wwwRootDirectoryPath)
        {
            var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", wwwRootDirectoryPath);
            var directoryToSearch = new DirectoryInfo(fileDirectory);
            var filesToDelete = directoryToSearch.GetFiles($"{keyId}*.*").ToList();
            
            filesToDelete.ForEach(file => file.Delete());
        }

        public string GetFileExtension(IFormFile filePath) =>
            Path.GetExtension(filePath.FileName);

        private async Task PersistFileToPath(IFormFile formFile, string filePath)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await formFile.CopyToAsync(fileStream);
        }

        private void ArchiveFile(string filePath)
        {
            var fileExtension = Path.GetExtension(filePath);
            if (File.Exists(filePath))
            {
                var fileInfo = new FileInfo(filePath);
                fileInfo.MoveTo(Path.ChangeExtension(filePath, $"{fileExtension}.tmp"), overwrite: true);
            }
        }

        private async Task<IFormFile> ProcessImage(IFormFile formFile, ImageSizeEnum imageSizeEnum)
        {
            var mediaDimensions = ImageSizeTypes.GetImageSizeDimensions(imageSizeEnum);

            using var image = await formFile.GetDrawingImage();
            var resizedImage = _mediaProcessingService.ResizeImage(image, mediaDimensions.X, mediaDimensions.Y);
            var watermarkImage = GetWatermarkImage();
            var watermarkedImage = _mediaProcessingService.AddWatermarkSystemDrawing(resizedImage, watermarkImage);

            var watermarkedImageStream = watermarkedImage.ToStream(ImageFormat.Jpeg);
            return new FormFile(watermarkedImageStream, 0, watermarkedImageStream.Length, null, formFile.FileName);
        }

        private DrawingImage GetWatermarkImage()
        {
            var watermarkUrl = $"{DomainUrls.Client}/bai.media.staticfiles/predefined/watermarks/default.png";
            return MediaService.DownloadImageFromUrl(watermarkUrl);
        }
    }
}
