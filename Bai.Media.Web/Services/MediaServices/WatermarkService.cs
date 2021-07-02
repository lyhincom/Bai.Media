using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using ImageMagick;

namespace Bai.Media.Web.Services.MediaServices
{
    public class WatermarkService
    {
        public MagickImage AddWatermark(Stream imageStream, Stream watermarkStream)
        {
            using var image = new MagickImage(imageStream);
            using var watermark = new MagickImage(watermarkStream);

            try
            {
                var size = new MagickGeometry(100, 100);
                size.IgnoreAspectRatio = true;
                image.Resize(size);

                image.Composite(watermark, watermark.Width, watermark.Height, CompositeOperator.Over);
                image.Write(watermarkStream);
            }
            catch (Exception e)
            {
                // Output file write error --- out of disk space? `' @ error/jpeg.c/JPEGErrorHandler/346

                // WriteBlob Failed `' @ error/png.c/MagickPNGErrorHandler/1715
                // https://github.com/dlemstra/Magick.NET/issues/562
                var x = e.Message;
                throw;
            }

            return image;
        }

        public Image AddWatermark2(Image image, Image watermark)
        {
            using (Graphics imageGraphics = Graphics.FromImage(image))
            using (var watermarkBrush = new TextureBrush(watermark))
            {
                int x = (image.Width - 30 - watermark.Width);
                int y = (image.Height - 20 - watermark.Height);

                // https://stackoverflow.com/questions/4113900/c-sharp-add-watermark-to-the-photo-by-special-way
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

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
