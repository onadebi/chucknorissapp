using chuck_swapi.ApplicationLib.Config;
using chuck_swapi.ApplicationLib.Core;
using chuck_swapi.DomainLib.Chuck;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace chuck_swapi.ApplicationLib.Modules.Chuck
{
    public class RandomJoke
    {
        public class QueryDetail : IRequest<GenResponse<ChuckJokes>>
        {
            public string Category { get; set; }
        }

        public class Handler : IRequestHandler<QueryDetail, GenResponse<ChuckJokes>>
        {
            private readonly IApiCall _apiCall;
            private readonly AppSettings _appSettings;

            public Handler(IApiCall _apiCall, IOptions<AppSettings> _appSettings)
            {
                this._apiCall = _apiCall;
                this._appSettings = _appSettings.Value;
            }

            public async Task<GenResponse<ChuckJokes>> Handle(QueryDetail request, CancellationToken cancellationToken)
            {
                ChuckJokes objResp = new();
                try
                {
                    var chuckResponse = await _apiCall.RestCall(_appSettings.BaseNoris, $"{_appSettings.ChuckRandomResource}{request.Category}", -1, cancellationToken);
                    if (chuckResponse != null && chuckResponse.IsSuccessful)
                    {
                        objResp = JsonConvert.DeserializeObject<ChuckJokes>(chuckResponse.Content) ?? new();
                    }
                    else if (chuckResponse != null && chuckResponse.StatusCode == HttpStatusCode.NotFound)
                    { return GenResponse<ChuckJokes>.Result(null, true, $"No records found for {request.Category}"); }
                    else { objResp = null; }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, EventLoggerType.ERROR);
                    objResp = null;
                }
                return objResp == null ? GenResponse<ChuckJokes>.Result(null, false) : GenResponse<ChuckJokes>.Result(objResp, true);
            }
        }
    }
}
