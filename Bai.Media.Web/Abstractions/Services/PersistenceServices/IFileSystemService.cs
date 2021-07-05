using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Abstractions.Services
{
    public interface IFileSystemService
    {
        Task AddFileToWwwRoot(IFormFile formFile, string wwwRootDirectoryPath, params string[] fileNames);
        void ArchiveWwwRootFile(string wwwRootDirectoryPath, params string[] fileName);
        string GetFileExtension(IFormFile filePath);
    }
}