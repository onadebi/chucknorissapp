using chuck_swapi.ApplicationLib.Modules.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chuck_swapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> categories(string query, CancellationToken ct)
        {
            return HandleResult(await BaseMediator.Send(new SearchList.QueryList() { search= query}, ct));
        }
    }
}
