using System.Net;
using HoangTramHuynh.Core.API;
using HoangTramHuynh.Core.Report;
using HoangTramHuynh.Models.Response;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace HoangTramHuynh.Services
{
    public class AccountService
    {
        private readonly APIClient _apiClient;
        private const string LoginEndpoint = "/Account/v1/Login";
        private const string UserEndpoint = "/Account/v1/User";

        public AccountService(APIClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<AccountResponse> LoginAsync(string username, string password)
        {
            ReportLog.Info($"Start logging in with username '{username}'.");

            var requestBody = new {userName = username, password = password};
            ReportLog.Info($"Request Body: {JsonConvert.SerializeObject(requestBody)}");

            RestResponse response = await _apiClient.PostAsync(LoginEndpoint,requestBody);
            ReportLog.Info($"Response Status Code: {(int)response.StatusCode} {response.StatusCode}");
            ReportLog.Info($"Response Content: {response.Content}");

            Assert.That(response.StatusCode,Is.EqualTo(HttpStatusCode.OK),$"Login API failed. Status: {response.StatusCode}, Content: {response.Content}");
            ReportLog.Info($"Login API succeeded. Status: {response.StatusCode}, Content: {response.Content}");

            AccountResponse accountResponse =JsonConvert.DeserializeObject<AccountResponse>(response.Content ?? string.Empty)?? throw new Exception("Cannot deserialize login response.");
            ReportLog.Info($"Response body is deserialized successfully.");

            Assert.That(accountResponse.UserId, Is.Not.Empty, "UserId is empty.");
            ReportLog.Info($"UserId is generated successfully. UserId = {accountResponse.UserId}");

            Assert.That(accountResponse.Token, Is.Not.Empty, "Token is empty.");
            ReportLog.Info($"Token is generated successfully.");

            ReportLog.Info("Login successfully.");
            return accountResponse;
        }
        public async Task<AccountResponse> GetUserByIdAsync(string userId, string token)
        {
            var headers = new Dictionary<string, string>{{ "Authorization", $"Bearer {token}" }};
            ReportLog.Info($"Start getting user by ID '{userId}'.");

            RestResponse response = await _apiClient.GetAsync($"{UserEndpoint}/{userId}",headers);
            ReportLog.Info($"Response Status Code: {(int)response.StatusCode} {response.StatusCode}");
            ReportLog.Info($"Response Content: {response.Content}");

            Assert.That(response.StatusCode,Is.EqualTo(HttpStatusCode.OK),$"Get user failed. Status: {response.StatusCode}, Content: {response.Content}");

            AccountResponse accountResponse =JsonConvert.DeserializeObject<AccountResponse>(response.Content ?? string.Empty)
                ?? throw new Exception("Cannot deserialize get user response.");
            ReportLog.Info($"Response body is deserialized successfully.");

            return accountResponse;
        }
        public async Task<bool> IsBookExistingInCollectionAsync(string userId,string token,string isbn)
        {
            AccountResponse accountResponse = await GetUserByIdAsync(userId, token);
            ReportLog.Info($"Checking if book with ISBN '{isbn}' exists in user's collection.");
            
            ReportLog.Info($"User's collection contains {accountResponse.Books.Count} books.");
            return accountResponse.Books.Any(book =>book.Isbn.Equals(isbn, StringComparison.OrdinalIgnoreCase));
        }
    }
}