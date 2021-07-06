using System;
using System.IO;
using Bai.Media.Web.Abstractions.Models;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Web.Models
{
    public class Image : IFormImage
    {
        public Guid PageId { get; set; }
        public string PageType { get; set; }

        public IFormFile FormImage { get; set; }

        internal static object FromStream(MemoryStream memoryStream)
        {
            throw new NotImplementedException();
        }
    }
}
