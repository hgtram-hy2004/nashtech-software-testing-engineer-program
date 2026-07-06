using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace HoangTramHuynh.Core.API
{
    public class ApiClient
    {
        private readonly RestClient _restClient;

        public ApiClient(string baseUrl)
        {
            _restClient = new RestClient(baseUrl);
        }

        public async Task<RestResponse> GetAsync(string endpoint, Dictionary<string, string>? headers = null)
        {
            var request = BuildRequest(endpoint, Method.Get, headers);
            return await _restClient.ExecuteAsync(request);
        }

        public async Task<RestResponse> PostAsync(string endpoint, object body, Dictionary<string, string>? headers = null)
        {
            var request = BuildRequest(endpoint, Method.Post, headers);
            request.AddJsonBody(body);
            return await _restClient.ExecuteAsync(request);
        }

        public async Task<RestResponse> PutAsync(string endpoint, object body, Dictionary<string, string>? headers = null)
        {
            var request = BuildRequest(endpoint, Method.Put, headers);
            request.AddJsonBody(body);
            return await _restClient.ExecuteAsync(request);
        }

        public async Task<RestResponse> DeleteAsync(string endpoint, Dictionary<string, string>? headers = null)
        {
            var request = BuildRequest(endpoint, Method.Delete, headers);
            return await _restClient.ExecuteAsync(request);
        }

        private static RestRequest BuildRequest(string endpoint, Method method, Dictionary<string, string>? headers)
        {
            var request = new RestRequest(endpoint, method);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            if (headers == null)
            {
                return request;
            }

            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            return request;
        }
    }
}