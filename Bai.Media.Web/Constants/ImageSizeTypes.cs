using System;
using System.Drawing;
using Bai.Media.Web.Enums;

namespace Bai.Media.Web.Constants
{
    public static class ImageSizeTypes
    {
        public const string Thumbnail = "s"; // "thumbnail"
        public const string Medium = "m"; // "medium"

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

        public static Point GetImageSizeDimensions(ImageSizeEnum imageSizeEnum)
        {
            switch (imageSizeEnum)
            {
                case ImageSizeEnum.Thumbnail:
                    return new Point(432, 243);
                case ImageSizeEnum.Medium:
                    return new Point(745, 419);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
