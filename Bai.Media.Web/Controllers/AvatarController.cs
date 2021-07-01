using System;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai.Media.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvatarController
    {
        private readonly IDomainRepository<Avatar, Guid> _repository;

        public AvatarController(IDomainRepository<Avatar, Guid> repository) =>
            _repository = repository;

        //[HttpPost] // [FromForm(Name = "file")] IFormFile formImage
        //public virtual async Task<ActionResult> Post([FromBody] Avatar entity) =>
        //    Ok(await _repository.AddEntity(entity, true));
    }
}
