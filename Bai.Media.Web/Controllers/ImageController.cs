using System;
using Bai.General.API.Controllers;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.Media.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bai.Media.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : DomainController<Image, Guid>
    {
        public ImageController(IDomainRepository<Image, Guid> repository) : base(repository)
        {
        }
    }
}
