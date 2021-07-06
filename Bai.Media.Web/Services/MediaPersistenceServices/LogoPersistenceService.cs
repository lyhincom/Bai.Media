using System;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Models;
using Bai.Media.Web.Services.MediaPersistenceServices.Base;

namespace Bai.Media.Web.Services.MediaPersistenceServices
{
    public class LogoPersistenceService : PersistenceService<Logo, LogoEntity>
    {
        public LogoPersistenceService(IDomainRepository<LogoEntity, Guid> repository,
                                      IFormImageToEntityConverterService<Logo, LogoEntity> baseImageService,
                                      IFileSystemService fileSystemService) : base(repository, baseImageService, fileSystemService)
        {
        }

        protected override string EntityName => "Logo";

        protected override Guid GetModelKeyId(Logo model) =>
            model.PageId;

        protected override string GetModelKeyAsString(Logo model) =>
            model.PageId.ToString();

        protected override void SetKeyFromModelToEntity(Logo model, LogoEntity newMediaEntity) =>
            newMediaEntity.PageId = model.PageId;
    }
}
