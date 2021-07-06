using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Bai.Media.Web.Abstractions.Services;

namespace Bai.Media.Web.Services.MediaProcessingServices
{
    public class MediaProcessingService : IMediaProcessingService
    {
        public Image AddWatermarkSystemDrawing(Image image, Image watermark)
        {
            using (Graphics imageGraphics = Graphics.FromImage(image))
            using (var watermarkBrush = new TextureBrush(watermark))
            {
                var x = image.Width - 30 - watermark.Width;
                var y = image.Height - 20 - watermark.Height;

                watermarkBrush.TranslateTransform(x, y);
                imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(watermark.Width + 1, watermark.Height)));
                return image;
            }
        }

        public Bitmap ResizeImage(Image image, int width = 845, int height = 475) // int width = 961, int height = 541
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

            return destImage;
        }
    }
}
