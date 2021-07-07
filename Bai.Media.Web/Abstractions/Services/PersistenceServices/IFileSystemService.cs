using System;
using System.Threading.Tasks;
using Bai.Media.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Abstractions.Services
{
    public interface IFileSystemService
    {
        Task AddFileToWwwRoot(IFormFile formFile, string wwwRootDirectoryPath, params MediaFileSystem[] fileNames);
        void ArchiveWwwRootFile(string wwwRootDirectoryPath, string[] fileNamesNoExtension, string fileExtension);
        string GetFileExtension(IFormFile filePath);
        void DeleteWwwRootMedia(Guid keyId, string wwwRootDirectoryPath);
    }
}