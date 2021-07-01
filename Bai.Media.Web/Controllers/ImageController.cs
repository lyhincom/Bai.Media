using System;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai.Media.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IDomainRepository<ImageEntity, Guid> _repository;

        public ImageController(IDomainRepository<ImageEntity, Guid> repository) =>
            _repository = repository;

        //[HttpPost]
        //public virtual async Task<ActionResult> Post([FromBody] TModel entity) =>
        //    Ok(await _repository.AddEntity(entity, true));
    }
}
