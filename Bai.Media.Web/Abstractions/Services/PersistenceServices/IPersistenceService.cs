using System.Threading.Tasks;
using Bai.Media.Web.Models;

namespace Bai.Media.Web.Abstractions.Services.PersistenceServices
{
    public interface IPersistenceService<TModel>
        where TModel : class
    {
        Task<MediaUrl> AddOrUpdateUserAvatar(TModel model);
    }
}