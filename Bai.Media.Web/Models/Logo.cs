using System;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Models
{
    public class Logo
    {
        public Guid PageId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
