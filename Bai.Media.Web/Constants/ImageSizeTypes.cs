using System;
using Bai.Media.Web.Enums;

namespace Bai.Media.Web.Constants
{
    public static class ImageSizeTypes
    {
        public const string Thumbnail = "thumbnail";
        public const string Medium = "medium";

        public static string GetImageSizePrefix(ImageSizeEnum imageSizeEnum)
        {
            switch (imageSizeEnum)
            {
                case ImageSizeEnum.Thumbnail:
                    return Thumbnail;
                case ImageSizeEnum.Medium:
                    return Medium;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
