using ImageMagick;

namespace Bai.Media.Web.Abstractions.Services
{
    public interface IMagicImageValidationService<TModel>
    {
        void ValidateImage(MagickImage image);
    }
}
