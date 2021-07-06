using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Abstractions;
using Bai.General.DAL.Abstractions.Models;
using Bai.Media.DAL.Abstractions.Models;
using Bai.Media.Web.Abstractions.Models;
using Bai.Media.Web.Enums;
using Bai.Media.Web.Models;

namespace Bai.Media.Web.Abstractions.Services.PersistenceServices
{
    public interface IPersistenceService<TModel, TEntity>
        where TModel : IFormImage
        where TEntity : GuidEntity, IImage, IImageUrls, ISoftDelete, new()
    {
        Task<MediaUrl> AddOrUpdateUserMedia(TModel model, Expression<Func<TEntity, bool>> whereExpression, ImageSizeEnum[] imageSizes = null);
    }
}