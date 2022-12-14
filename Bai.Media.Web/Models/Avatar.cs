using System;
using Bai.Media.Web.Abstractions.Models;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Models
{
    public class Avatar : IFormImage
    {
        public Guid UserId { get; set; }

        public IFormFile FormImage { get; set; }
    }
}
