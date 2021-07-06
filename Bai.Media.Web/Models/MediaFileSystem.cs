using Bai.Media.Web.Enums;

namespace Bai.Media.Web.Models
{
    public class MediaFileSystem
    {
        public MediaFileSystem()
        { 
        }

        public MediaFileSystem(string mediaPath) =>
            MediaPath = mediaPath;

        public MediaFileSystem(string mediaPath, ImageSizeEnum? mediaSize)
        {
            MediaPath = mediaPath;
            MediaSize = mediaSize;
        }

        public string MediaPath { get; set; }
        public ImageSizeEnum? MediaSize { get; set; }
    }
}
