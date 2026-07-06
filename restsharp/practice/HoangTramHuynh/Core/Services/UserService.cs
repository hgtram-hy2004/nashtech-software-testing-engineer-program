using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoangTramHuynh.Core.API;
using HoangTramHuynh.DataObjects;
using RestSharp;

namespace HoangTramHuynh.Core.Services
{
    public class UserService
    {
        private readonly ApiClient _apiClient;
        private readonly string _token;
        private const string UsersEndpoint = "/public/v2/users";
        public UserService(ApiClient apiClient, string token)
        {
            _apiClient = apiClient;
            _token = token;
        }

        public async Task<RestResponse> GetUsersAsync()
        {
            return await _apiClient.GetAsync(UsersEndpoint);
        }

        public async Task<RestResponse> GetUserByIdAsync(int userId)
        {
            return await _apiClient.GetAsync($"{UsersEndpoint}/{userId}",GetAuthorizationHeader());
        }

        public async Task<RestResponse> CreateUserAsync(UserRequestDto user)
        {
            return await _apiClient.PostAsync(UsersEndpoint,user,GetAuthorizationHeader());
        }

        public async Task<RestResponse> UpdateUserByPutAsync(int userId, UserRequestDto user)
        {
            return await _apiClient.PutAsync($"{UsersEndpoint}/{userId}",user,GetAuthorizationHeader());
        }

        public async Task<RestResponse> DeleteUserAsync(int userId)
        {
            return await _apiClient.DeleteAsync($"{UsersEndpoint}/{userId}",GetAuthorizationHeader());
        }

        private Dictionary<string, string> GetAuthorizationHeader()
        {
            return new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {_token}" }
            };
        }
    }
}