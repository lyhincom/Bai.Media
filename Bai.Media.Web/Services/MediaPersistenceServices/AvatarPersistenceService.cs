using System;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Models;
using Bai.Media.Web.Services.MediaPersistenceServices.Base;

namespace Bai.Media.Web.Services.MediaPersistenceServices
{
    public class AvatarPersistenceService : PersistenceService<Avatar, AvatarEntity>
    {
        public AvatarPersistenceService(IDomainRepository<AvatarEntity, Guid> repository,
                                        IFormImageToEntityConverterService<Avatar, AvatarEntity> baseImageService,
                                        IFileSystemService fileSystemService) : base(repository, baseImageService, fileSystemService)
        {
        }

        protected override string EntityName => "Avatar";

        protected override Guid GetModelKey(Avatar model) =>
            model.UserId;

        protected override string GetEntityKeyAsString(AvatarEntity entity) =>
            entity.UserId.ToString();

        protected override void SetKeyFromModelToEntity(Avatar model, AvatarEntity newMediaEntity) =>
            newMediaEntity.UserId = model.UserId;
    }
}
