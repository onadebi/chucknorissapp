using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace chuck_swapi.ApplicationLib.Core
{
    public interface IApiCall
    {
        Task<RestClientResponse> RestCall(string BaseUrl, string reqResource = "", int timeout = -1, CancellationToken ct = default!);
    }
    public class ApiCall : IApiCall
    {
        public async Task<RestClientResponse> RestCall(string BaseUrl, string reqResource, int timeout, CancellationToken ct)
        {
            RestClientResponse restResponse = new RestClientResponse();
            try
            {
                var clientOptions = new RestClientOptions(BaseUrl)
                {
                    Timeout = timeout,
                };

                var client = new RestClient(clientOptions);
                var restRequest = new RestRequest($"{BaseUrl}{reqResource}");
                RestResponse response = await client.ExecuteAsync(restRequest, ct);

                restResponse.IsSuccessful = response.IsSuccessful;
                restResponse.Content = response.Content;
                restResponse.StatusCode = response.StatusCode;
                if (!response.IsSuccessful)
                {
                    restResponse.ErrorMessage = response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"{ex.Message} - [{BaseUrl}{reqResource}]", EventLoggerType.ERROR);
                restResponse.ErrorException = ex;
                restResponse.ErrorMessage = ex.Message;
            }
            return restResponse;
        }
    }

    public class RestClientResponse
    {
        public bool IsSuccessful { get; set; }  
        public string ErrorMessage { get; set; } = default!;
        public string Content { get; set; } = default!;

        public HttpStatusCode StatusCode { get; set; } = default!;

        public Exception ErrorException { get; set; } = default!;

    }
}
