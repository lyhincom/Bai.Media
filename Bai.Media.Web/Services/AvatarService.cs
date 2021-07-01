using System;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Models;
using Bai.Media.Web.Services.Base;
using ImageMagick;

namespace Bai.Media.Web.Services
{
    public class AvatarService : BaseImageService<Avatar, AvatarEntity>
    {
        public override void ValidateImage(MagickImage image)
        {
            if (image.Width != image.Height)
            {
                throw new Exception("Avatar height must be equal to width, 1:1 size proportion condition is not fulfilled.");
            }

            if (image.Width < 64)
            {
                throw new Exception("Avatar is too small, cannot be less than 64px.");
            }

            if (image.Width > 500)
            {
                throw new Exception("Avatar is too large, cannot be less than 64px.");
            }
        }
    }
}
