using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions.Models;
using Bai.Media.Web.Abstractions.Models;

namespace Bai.Media.Web.Abstractions.Services
{
    public interface IFormImageToEntityConverterService<TModel, TEntity>
        where TEntity : GuidEntity, IImage
    {
        TEntity GetEntityFromFormImageAndValidate(IFormImage model);
    }
}