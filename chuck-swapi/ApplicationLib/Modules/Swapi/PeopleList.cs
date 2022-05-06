﻿using chuck_swapi.ApplicationLib.Config;
using chuck_swapi.ApplicationLib.Core;
using chuck_swapi.DomainLib.Swapi;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace chuck_swapi.ApplicationLib.Modules.Swapi
{
    public class PeopleList
    {

        public class QueryList : IRequest<GenResponse<StarWarsList>> { }

        public class Handler : IRequestHandler<QueryList, GenResponse<StarWarsList>>
        {
            private readonly AppSettings _appSettings;
            private readonly IApiCall _apiCall;

            public Handler(IOptions<AppSettings> _appSettings, IApiCall _apiCall)
            {
                this._appSettings = _appSettings.Value;
                this._apiCall = _apiCall;
            }

            public async Task<GenResponse<StarWarsList>> Handle(QueryList request, CancellationToken cancellationToken)
            {
                StarWarsList starWarsList = new StarWarsList();
                try
                {
                    var response = await _apiCall.RestCall(_appSettings.BaseSwapi, _appSettings.SwapiPeopleResource, -1, cancellationToken);
                    if (response != null && response.IsSuccessful)
                    {
                        starWarsList = JsonConvert.DeserializeObject<StarWarsList>(response.Content);
                        return GenResponse<StarWarsList>.Result(starWarsList, true);
                    }
                    else
                    {
                        return GenResponse<StarWarsList>.Result(null, false, response?.ErrorMessage); ; ;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message, EventLoggerType.ERROR);
                }
                return GenResponse<StarWarsList>.Result(null, false);
            }
        }
    }
}