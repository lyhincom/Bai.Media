using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions.Models;
using Bai.Media.Web.Abstractions.Models;
using ImageMagick;

namespace Bai.Media.Web.Abstractions.Services
{
    public interface IMagicImageValidationService<TModel, TEntity>
        where TModel : IFormImage
        where TEntity : GuidEntity, IImage, new()
    {
        void ValidateImage(MagickImage image);
    }
}
