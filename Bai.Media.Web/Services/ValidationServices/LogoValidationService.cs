using System;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Exceptions.Base;
using Bai.Media.Web.Models;
using ImageMagick;

namespace Bai.Media.Web.Services
{
    public class LogoValidationService : IMagicImageValidationService<Logo>
    {
        private const string EntityName = "Logo";
        private const int DimensionMinimum = 100;
        private const int DimensionMaximum = 500;

        public void ValidateImage(MagickImage image)
        {
            ValidateDimension(image.Width, "width");
            ValidateDimension(image.Height, "height");
        }

        private void ValidateDimension(int pixelSize, string dimensionName)
        {
            if (pixelSize < DimensionMinimum)
            {
                throw new MediaValidationException(string.Format("Uploaded {0} {1} is too small, cannot be less than {2}px. Current value is {3}px.", EntityName, dimensionName, DimensionMinimum, pixelSize));
            }

            if (pixelSize > DimensionMaximum)
            {
                throw new MediaValidationException(string.Format("Uploaded {0} {1} is too large, cannot be greater than {2}px. Current value is {3}px.", EntityName, dimensionName, DimensionMaximum, pixelSize));
            }
        }
    }
}
