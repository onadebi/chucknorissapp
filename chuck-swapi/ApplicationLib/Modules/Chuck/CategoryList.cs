using chuck_swapi.ApplicationLib.Config;
using chuck_swapi.ApplicationLib.Core;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace chuck_swapi.ApplicationLib.Modules.Chuck
{
    public class CategoryList
    {
        public class QueryList : IRequest<GenResponse<List<string>>> { }

        public class Handler : IRequestHandler<QueryList, GenResponse<List<string>>>
        {
            private readonly AppSettings _appSettings;
            private readonly IApiCall _apiCall;

            public Handler(IOptions<AppSettings> _appSettings, IApiCall _apiCall)
            {
                this._appSettings = _appSettings.Value;
                this._apiCall = _apiCall;
            }
            public async Task<GenResponse<List<string>>> Handle(QueryList request, CancellationToken cancellationToken)
            {
                List<string> list = new List<string>();
                try
                {
                    var response = await _apiCall.RestCall(_appSettings.BaseNoris, _appSettings.NorisJokeCategories);
                    if (response != null && response.IsSuccessful)
                    {
                        list = JsonConvert.DeserializeObject<List<string>>(response.Content) ?? new List<string>();
                        return GenResponse<List<string>>.Result(list, true);
                    }
                    else
                    {
                        return GenResponse<List<string>>.Result(null, false, response?.ErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, EventLoggerType.ERROR);
                }
                return GenResponse<List<string>>.Result(null, false);
            }
        }
    }

}
