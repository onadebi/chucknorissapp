using chuck_swapi.ApplicationLib.Modules.Chuck;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chuck_swapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuckController : BaseController
    {

        [HttpGet("categories")]
        public async Task<IActionResult> categories(CancellationToken ct)
        {
            var result = HandleResult( await BaseMediator.Send(new CategoryList.QueryList(),ct));
            return result;
        }

        [HttpGet("randomjoke/{category}")]
        public async Task<IActionResult> Random(string category, CancellationToken ct)
        {
            return HandleResult(await BaseMediator.Send(new RandomJoke.QueryDetail() {Category = category }, ct));
        }
    }
}
