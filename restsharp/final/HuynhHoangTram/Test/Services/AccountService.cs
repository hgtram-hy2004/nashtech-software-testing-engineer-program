using HuynhHoangTram.Core.API;
using RestSharp;
using Newtonsoft.Json;
using HuynhHoangTram.Models.Request;
using HuynhHoangTram.Models.Response;

namespace HuynhHoangTram.Core.Services
{
    public class AccountService
    {
        private readonly APIClient _apiClient;
        private const string AccountEndpoint = "/Account/v1/User";
        private const string GenerateTokenEndpoint = "/Account/v1/GenerateToken";
        public AccountService(APIClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<RestResponse> GetUserByIdAsync(string userId, string? token = null)
        {
            return await _apiClient.GetAsync(
                $"{AccountEndpoint}/{userId}",
                GetAuthorizationHeader(token)
            );
        }
        public async Task<string> GenerateTokenAsync(AccountRequest accountRequest)
        {
            var response = await _apiClient.PostAsync(
                GenerateTokenEndpoint,
                accountRequest
            );

            var tokenResponse = JsonConvert.DeserializeObject<GenerateTokenResponse>(response.Content!);

            return tokenResponse!.Token;
        }
        private Dictionary<string, string> GetAuthorizationHeader(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return new Dictionary<string, string>();
            }

            return new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };
        }
    }
}