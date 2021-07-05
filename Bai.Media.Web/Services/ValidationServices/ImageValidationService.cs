using System;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Models;
using ImageMagick;

namespace Bai.Media.Web.Services
{
    public class ImageValidationService : IMagicImageValidationService<Image, ImageEntity>
    {
        private const string EntityName = "Image";
        public const int ConstWidth = 942;
        public const int ConstHeight = 531;

        public void ValidateImage(MagickImage image)
        {
            if (image.Width == ConstWidth)
            {
                throw new Exception($"{EntityName} size must be 942x531. Uploaded {EntityName.ToLower()} width {image.Width}px is different from {ConstWidth}px.");
            }

            if (image.Height == ConstHeight)
            {
                throw new Exception($"{EntityName} size must be 942x531. Uploaded {EntityName.ToLower()} height {image.Height}px is different from {ConstHeight}px.");
            }
        }
    }
}
