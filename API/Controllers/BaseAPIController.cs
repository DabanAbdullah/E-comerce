using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseAPIController : ControllerBase
    {
        protected async Task<ActionResult> CreatePageResult<T>(IGenericRepository<T> repo,
        ISpecification<T> spec, int PageIndex, int PageSie) where T : BaseEntity

        {
            var items = await repo.GetListWithSpecAsync(spec);
            var count = await repo.CountAsync(spec);
            var pagination = new Pagination<T>(PageIndex, PageSie, count, items);
            return Ok(pagination);


        }
    }
}
