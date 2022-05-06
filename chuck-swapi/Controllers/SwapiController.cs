using chuck_swapi.ApplicationLib.Modules.Swapi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chuck_swapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwapiController : BaseController
    {

        [HttpGet("people")]
        public async Task<IActionResult> categories(CancellationToken ct)
        {
            return HandleResult(await BaseMediator.Send(new PeopleList.QueryList(), ct));
        }
    }
}
