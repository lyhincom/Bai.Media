using System;
using System.Threading.Tasks;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai.Media.Web.Controllers
{
    /// <summary>
    /// Logo media has rectangle or square 1:1 proportion
    /// Logo media does not have a Watermark
    /// </summary>
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

        #region Debug

        [HttpDelete]
        public virtual async Task<ActionResult> Delete(Guid pageId, string pageType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
