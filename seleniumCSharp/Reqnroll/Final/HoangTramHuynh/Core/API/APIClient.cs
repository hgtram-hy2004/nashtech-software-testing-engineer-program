using RestSharp;
using HoangTramHuynh.Core.Report;

namespace HoangTramHuynh.Core.API
{
    public class APIClient
    {
        private readonly RestClient _restClient;

        public APIClient(string baseUrl)
        {
            _restClient = new RestClient(baseUrl);
        }
        public async Task<RestResponse> GetAsync(string endpoint,Dictionary<string, string>? headers = null)
        {
            var request = BuildRequest(endpoint, Method.Get, headers);
            ReportLog.Info($"Sending GET request to endpoint: {endpoint}");
            return await _restClient.ExecuteAsync(request);
        }

        public async Task<RestResponse> PostAsync(string endpoint,object body,Dictionary<string, string>? headers = null)
        {
            var request = BuildRequest(endpoint, Method.Post, headers);
            request.AddJsonBody(body);
            ReportLog.Info($"Sending POST request to endpoint: {endpoint}");
            return await _restClient.ExecuteAsync(request);
        }

        private static RestRequest BuildRequest( string endpoint, Method method,Dictionary<string, string>? headers)
        {
            var request = new RestRequest(endpoint, method);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            ReportLog.Info($"Building request for endpoint: {endpoint}");
            if (headers == null)
            {
                return request;
            }
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            ReportLog.Info($"Request headers: {string.Join(", ", request.Parameters.Select(p => $"{p.Name}: {p.Value}"))}");
            return request;
        }
    }
}