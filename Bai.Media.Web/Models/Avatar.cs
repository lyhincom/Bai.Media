using System;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Models
{
    public class Avatar
    {
        public Guid UserId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
