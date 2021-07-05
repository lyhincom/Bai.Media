using System;
using Bai.Domain.Settings.Getters;
using Bai.General.API;

namespace Bai.Media.Web.Services
{
    public static class MediaUrlService
    {
        public static string GetDatabaseUrl(Guid keyId, string controllerName) =>
            DomainUrl.Combine(DomainUrls.Client, "api", controllerName, keyId.ToString()).ToLower();

        public static string GetFileSystemUrl(string fileExtension, Guid keyId, string folderName) =>
            DomainUrl.Combine(DomainUrls.Client, "Bai.Media.StaticFiles", folderName, $"{keyId}{fileExtension}").ToLower();
    }
}
