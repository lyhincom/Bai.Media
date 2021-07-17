using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Bai.Domain.Settings.Getters;
using Bai.Media.Connector.Abstractions;
using Bai.Media.Connector.Models;
using Bai.Media.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Bai.Media.Connector.Services
{
    public class MediaConnector : IMediaConnector
    {
        public async Task<Validation<MediaUrl>> PostMedia(IFormFile formImage, string entityName, IDictionary<string, string> keyValues)
        {
            using var httpClient = new HttpClient();
            var multipartContent = new MultipartFormDataContent();
            keyValues.ToList().ForEach(pair => multipartContent.Add(new StringContent(pair.Value), pair.Key));
            multipartContent.Add(new StreamContent(formImage.OpenReadStream()), "Attachment", formImage.FileName);

            var response = await httpClient.PostAsync($"{DomainUrls.Client}/api/{entityName}", multipartContent);
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStringAsync();
                var mediaUrl = JsonSerializer.Deserialize<MediaUrl>(contentStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return new Validation<MediaUrl>(mediaUrl);
            }

            string message = await response.Content.ReadAsStringAsync();
            return new Validation<MediaUrl>(message);
        }

        public async Task<bool> TryDelete(Guid mediaId, string mediaType, string imageType = null)
        {
            try
            {
                await Delete(mediaId, mediaType, imageType);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task Delete(Guid mediaId, string mediaType, string imageType = null)
        {
            using var httpClient = new HttpClient();
            var requestUrl = BuildMediaUrl(mediaId, mediaType, imageType);
            var response = await httpClient.DeleteAsync(requestUrl);

            response.EnsureSuccessStatusCode();
        }

        private string BuildMediaUrl(Guid mediaId, string mediaType, string imageType = null)
        {
            switch (mediaType.ToLower())
            {
                case "avatar":
                    return $"{DomainUrls.Client}/api/avatar?userId={mediaId}";
                case "logo":
                    return $"{DomainUrls.Client}/api/logo?pageId={mediaId}";
                case "image":
                    return $"{DomainUrls.Client}/api/image?pageId={mediaId}";
            }

            throw new NotImplementedException();
        }
    }
}
