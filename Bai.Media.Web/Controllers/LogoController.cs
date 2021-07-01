using System;
using Bai.General.API.Controllers;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai.Media.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogoController : DomainController<Logo, Guid>
    {
        public LogoController(IDomainRepository<Logo, Guid> repository) : base(repository)
        {
        }

        //[HttpPost]
        //public virtual async Task<ActionResult> Post([FromBody] TModel entity) =>
        //    Ok(await _repository.AddEntity(entity, true));
    }
}
