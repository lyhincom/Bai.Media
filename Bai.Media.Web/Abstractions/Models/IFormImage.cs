using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Abstractions.Models
{
    public interface IFormImage
    {
        public IFormFile FormImage { get; set; }
    }
}
