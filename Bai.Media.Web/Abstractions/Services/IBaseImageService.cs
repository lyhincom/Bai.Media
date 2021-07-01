using Bai.Media.Web.Abstractions.Models;

namespace Bai.Media.Web.Abstractions.Services
{
    public interface IBaseImageService<TModel, TEntity>
    {
        TEntity GetFileMetadata(IFormImage model);
        string GetDatabaseUrl(TEntity entity, string controllerName);
        string GetFileSystemUrl(TEntity entity, string folderName);
    }
}