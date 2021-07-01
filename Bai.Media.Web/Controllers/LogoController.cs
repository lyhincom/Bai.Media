using System;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai.Media.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogoController : ControllerBase
    {
        private readonly IDomainRepository<LogoEntity, Guid> _repository;

        public LogoController(IDomainRepository<LogoEntity, Guid> repository) =>
            _repository = repository;

        //[HttpPost]
        //public virtual async Task<ActionResult> Post([FromBody] Logo entity) =>
        //    Ok(await _repository.AddEntity(entity, true));
    }
}
