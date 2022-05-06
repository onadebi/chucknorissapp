using chuck_swapi.ApplicationLib.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace chuck_swapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator BaseMediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public ActionResult HandleResult<T>(GenResponse<T> result)
        {
            if(result == null) return NotFound();
            if(result.IsSuccess && result.Data != null) return Ok(result);
            if(result.IsSuccess && result.Data == null) return NotFound(result);
            return BadRequest(result);
        }
    }
}
