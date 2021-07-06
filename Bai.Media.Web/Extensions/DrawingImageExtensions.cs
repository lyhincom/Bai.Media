using System.Drawing.Imaging;
using System.IO;
using DrawingImage = System.Drawing.Image;

namespace Bai.Media.Web.Extensions
{
    public static class DrawingImageExtensions
    {
        public static Stream ToStream(this DrawingImage image, ImageFormat format)
        {
            var stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;

            return stream;
        }
    }
}
