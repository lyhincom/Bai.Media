using System.Drawing;
using System.IO;

namespace Bai.Media.Web.Services
{
    public static class MediaService
    {
        public static Image GetImageFromByteArray(byte[] byteArray)
        {
            var memoryStream = new MemoryStream(byteArray, 0, byteArray.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return Image.FromStream(memoryStream);
        }

        public static byte[] ImageToByteArray(Image imageIn) =>
            (byte[])new ImageConverter().ConvertTo(imageIn, typeof(byte[]));
    }
}
