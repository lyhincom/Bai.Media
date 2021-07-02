using Bai.Media.DAL.Models;
using Bai.Media.Web.Models;
using Bai.Media.Web.Services.Base;
using ImageMagick;

namespace Bai.Media.Web.Services
{
    public class LogoService : BaseImageService<Logo, LogoEntity>
    {
        protected override void ValidateImage(MagickImage image)
        {
        }
    }
}
