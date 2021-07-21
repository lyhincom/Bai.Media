using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bai.Media.Connector.Models;
using Bai.Media.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Connector.Abstractions
{
    public interface IMediaConnector
    {
        Task<Validation<MediaUrl>> PostMedia(IFormFile formImage, string entityName, IDictionary<string, string> keyValues);
        Task<bool> ImageExists(Guid imageId, string imageType);
        Task<bool> TryDelete(Guid mediaId, string mediaType, string imageType = null);
        Task Delete(Guid mediaId, string mediaType, string imageType = null);
    }
}