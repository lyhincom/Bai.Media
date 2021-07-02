using System.IO;
using System.Threading.Tasks;
using Bai.Media.Web.Abstractions.Services;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Services
{
    public class FileSystemService : IFileSystemService
    {
        public async Task AddFileToWwwRoot(IFormFile formFile, string wwwRootDirectoryPath, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", wwwRootDirectoryPath, fileName);
            await AddFileToPath(formFile, filePath);
        }

        public async Task ArchiveWwwRootFile(string wwwRootDirectoryPath, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", wwwRootDirectoryPath, fileName);
            ArchiveFile(filePath);
        }

        public string GetFileExtension(IFormFile filePath) =>
            Path.GetExtension(filePath.FileName);

        private async Task AddFileToPath(IFormFile formFile, string filePath)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await formFile.CopyToAsync(fileStream);
        }

        private void ArchiveFile(string filePath)
        {
            var fileExtension = Path.GetExtension(filePath);
            var fileInfo = new FileInfo(filePath);
            fileInfo.MoveTo(Path.ChangeExtension(filePath, $"{fileExtension}.tmp"), overwrite: true);
        }
    }
}
