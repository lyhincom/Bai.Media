using System;
using Bai.Media.Web.Abstractions.Models;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Models
{
    public class Image : IFormImage
    {
        public Guid UserId { get; set; }
        public Guid PageId { get; set; }
        public string PageType { get; set; }

        public IFormFile FormImage { get; set; }
    }
}
