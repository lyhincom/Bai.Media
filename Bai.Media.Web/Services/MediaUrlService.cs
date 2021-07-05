﻿using System;
using Bai.Domain.Settings.Getters;
using Bai.General.API;
using Bai.Media.Web.Enums;

namespace Bai.Media.Web.Services
{
    public static class MediaUrlService
    {
        public static string GetDatabaseUrl(Guid keyId, string controllerName) =>
            DomainUrl.Combine(DomainUrls.Client, "api", controllerName, keyId.ToString()).ToLower();

        public static string GetFileSystemUrl(string folderName, string fileName) =>
            DomainUrl.Combine(DomainUrls.Client, "Bai.Media.StaticFiles", folderName, fileName).ToLower();

        public static string GetFileName(Guid keyId, string fileExtension, ImageSizeEnum? imageSize = null) =>
            imageSize.HasValue ?
                $"{keyId}{fileExtension}" :
                $"{keyId}_{ImageSizeTypes.GetImageSizePrefix(imageSize.Value)}{fileExtension}";
    }
}
