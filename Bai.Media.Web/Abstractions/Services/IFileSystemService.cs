using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Abstractions.Services
{
    public interface IFileSystemService
    {
        Task AddFileToWwwRoot(IFormFile formFile, string wwwRootDirectoryPath, string fileName);
        Task ArchiveWwwRootFile(string wwwRootDirectoryPath, string fileName);
        string GetFileExtension(IFormFile filePath);
    }
}