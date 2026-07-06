using HuynhHoangTram.Core.API;
using HuynhHoangTram.Models.Request;
using RestSharp;

namespace HuynhHoangTram.Core.Services
{
    public class BookStoreService
    {
        private readonly APIClient _apiClient;
        private const string BooksEndpoint = "/BookStore/v1/Books";
        private const string BookEndpoint = "/BookStore/v1/Book";

        public BookStoreService(APIClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<RestResponse> AddBookToCollectionAsync(BookStoreRequest bookRequest, string? token = null)
        {
            return await _apiClient.PostAsync(
                BooksEndpoint,
                bookRequest,
                GetAuthorizationHeader(token)
            );
        }

        public async Task<RestResponse> DeleteBookFromCollectionAsync(BookStoreRequest bookRequest, string? token = null )
        {
            return await _apiClient.DeleteAsync(
                BookEndpoint,
                bookRequest,
                GetAuthorizationHeader(token)
            );
        }

        public async Task<RestResponse> ReplaceBookInCollectionAsync(string oldIsbn, BookStoreRequest bookRequest, string? token = null )
        {
            return await _apiClient.PutAsync(
                $"{BooksEndpoint}/{oldIsbn}",
                bookRequest,
                GetAuthorizationHeader(token)
            );
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