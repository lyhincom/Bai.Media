using System.Threading.Tasks;
using Bai.Media.Web.Abstractions.Models;
using Bai.Media.Web.Models;

namespace Bai.Media.Web.Abstractions.Services.PersistenceServices
{
    public interface IPersistenceService<TModel> where TModel : IFormImage
    {
        Task<MediaUrl> AddOrUpdateUserMedia(TModel model);
    }
}