using System;
using Bai.Media.Web.Abstractions.Models;

namespace Bai.Media.Web.Abstractions.Services
{
    public interface IBaseImageService<TModel, TEntity>
    {
        TEntity GetFileMetadata(IFormImage model);
        string GetDatabaseUrl(TEntity entity, Guid keyId, string controllerName);
        string GetFileSystemUrl(TEntity entity, Guid keyId, string folderName);
    }
}