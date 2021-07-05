using System.Drawing;
using System.IO;
using ImageMagick;

namespace Bai.Media.Web.Abstractions.Services
{
    public interface IMediaProcessingService
    {
        MagickImage AddWatermarkMagickImage(Stream imageStream, Stream watermarkStream);
        Image AddWatermarkSystemDrawing(Image image, Image watermark);
        Bitmap ResizeImage(Image image, int width = 845, int height = 475);
    }
}