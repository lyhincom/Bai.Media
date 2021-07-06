using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DrawingImage = System.Drawing.Image;

namespace Bai.Media.Web.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<DrawingImage> GetDrawingImage(this IFormFile formFile)
        {
            var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return DrawingImage.FromStream(memoryStream);
        }
    }
}
