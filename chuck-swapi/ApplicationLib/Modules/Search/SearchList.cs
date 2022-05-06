using chuck_swapi.ApplicationLib.Config;
using chuck_swapi.ApplicationLib.Core;
using chuck_swapi.DomainLib.Chuck;
using chuck_swapi.DomainLib.Search;
using chuck_swapi.DomainLib.Swapi;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace chuck_swapi.ApplicationLib.Modules.Search
{
    public class SearchList
    {
        public class QueryList : IRequest<GenResponse<List<SearchResult>>> {
            public string search { get; set; }
        }

        public class Handler : IRequestHandler<QueryList, GenResponse<List<SearchResult>>>
        {
            private readonly IApiCall _apiCall;
            private readonly AppSettings _appSettings;

            public Handler(IApiCall _apiCall, IOptions<AppSettings> _appSettings)
            {
                this._apiCall = _apiCall;
                this._appSettings = _appSettings.Value;
            }

            public async Task<GenResponse<List<SearchResult>>> Handle(QueryList request, CancellationToken cancellationToken)
            {
                List<SearchResult> objResp = new List<SearchResult>();
                try
                {
                    var chuckResponse = await _apiCall.RestCall($"{_appSettings.BaseNoris}",$"{_appSettings.NorisSearch}{request.search}",-1, cancellationToken);
                    if (chuckResponse != null && chuckResponse.IsSuccessful)
                    {
                        var chuckSearchList = JsonConvert.DeserializeObject<ChuckJokesList>(chuckResponse.Content) ?? new();
                        chuckSearchList.Result.ForEach(result => { objResp.Add(new SearchResult { Content = result.Value, Metadata = _appSettings.ChuckMetaData });  });
                    }
                    var swapiResponse = await _apiCall.RestCall($"{_appSettings.BaseSwapi}",$"{_appSettings.SwapiPeopleResource}{_appSettings.SwapiSearchResource}{request.search}", -1, cancellationToken);
                    if (swapiResponse != null && swapiResponse.IsSuccessful)
                    {
                        var swapiSearchList = JsonConvert.DeserializeObject<StarWarsList>(swapiResponse.Content) ?? new();
                        swapiSearchList.Results.Where(m=>!string.IsNullOrWhiteSpace(m.Name)).ToList().ForEach(result => { objResp.Add(new SearchResult { Content = result.Name, Metadata = _appSettings.SwapiMetaData }); });
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, EventLoggerType.ERROR);
                }
                return objResp.Count > 0 ? GenResponse<List<SearchResult>>.Result(objResp, true) : GenResponse<List<SearchResult>>.Result(null, false);
            }
        }
    }
}
