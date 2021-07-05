using System;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Models;
using ImageMagick;

namespace Bai.Media.Web.Services.ValidationServices
{
    public class AvatarValidationService : IMagicImageValidationService<Avatar, AvatarEntity>
    {
        private const string EntityName = "Avatar";
        private const int DimensionMinimum = 64;
        private const int DimensionMaximum = 500;

        public void ValidateImage(MagickImage image)
        {
            if (image.Width != image.Height)
            {
                throw new Exception($"{EntityName} height must be equal to width, 1:1 size proportion condition is not fulfilled.");
            }

            if (image.Width < DimensionMinimum)
            {
                throw new Exception($"{EntityName} width is too small, cannot be less than {DimensionMinimum}px. Current value is: {image.Width}px.");
            }

            if (image.Width > DimensionMaximum)
            {
                throw new Exception($"{EntityName} width is too large, cannot be greater than {DimensionMaximum}px. Current value is: {image.Width}px.");
            }
        }
    }
}
