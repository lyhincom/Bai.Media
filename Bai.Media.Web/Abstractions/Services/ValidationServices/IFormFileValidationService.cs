using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Abstractions.Services
{
    public interface IFormFileValidationService
    {
        void ValidateFormFile(IFormFile formFile);
    }
}
