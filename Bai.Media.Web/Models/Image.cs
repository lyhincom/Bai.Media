using System;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Models
{
    public class Image
    {
        public Guid UserId { get; set; }
        public Guid PageId { get; set; }
        public string PageType { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
