using System;
using Bai.Media.Web.Abstractions.Models;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Models
{
    public class Logo : IFormImage
    {
        public Guid PageId { get; set; }

        public IFormFile FormImage { get; set; }
    }
}
