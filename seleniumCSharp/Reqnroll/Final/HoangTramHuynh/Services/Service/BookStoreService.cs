using System.Net;
using HoangTramHuynh.Core.API;
using HoangTramHuynh.Core.Report;
using HoangTramHuynh.Models.Request;
using HoangTramHuynh.Models.Response;
using Newtonsoft.Json;
using RestSharp;

namespace HoangTramHuynh.Services
{
    public class BookStoreService
    {
        private readonly APIClient _apiClient;
        private const string BooksEndpoint = "/BookStore/v1/Books";

        public BookStoreService(APIClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<RestResponse> GetBooksAsync()
        {
            return await _apiClient.GetAsync(BooksEndpoint);
        }
        public async Task<RestResponse> AddBookToCollectionAsync(BookStoreRequest bookRequest, string? token = null)
        {
            return await _apiClient.PostAsync(BooksEndpoint,bookRequest,GetAuthorizationHeader(token));
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
        public async Task<string> GetIsbnByBookNameAsync(string bookName)
        {
            RestResponse response = await GetBooksAsync();
            ReportLog.Info($"Response Status Code: {(int)response.StatusCode} {response.StatusCode}");
            ReportLog.Info($"Response Content: {response.Content}");

            Assert.That(response.StatusCode,Is.EqualTo(HttpStatusCode.OK),$"Get books failed. Status: {response.StatusCode}, Content: {response.Content}");
            ReportLog.Info($"Get books API succeeded. Status: {response.StatusCode}, Content: {response.Content}");

            BookStoreResponse bookStoreResponse = JsonConvert.DeserializeObject<BookStoreResponse>(response.Content ?? string.Empty)?? throw new Exception("Cannot deserialize book store response.");
            ReportLog.Info($"Response body is deserialized successfully.");

            BookStoreBookResponse? book = bookStoreResponse.Books.FirstOrDefault(book => book.Title.Equals(bookName, StringComparison.OrdinalIgnoreCase));
            ReportLog.Info($"Searching for book with name '{bookName}' in the book store response.");

            Assert.That(book,Is.Not.Null,$"Book '{bookName}' is not found in Book Store response.");
            Assert.That(book!.Isbn,Is.Not.Empty,$"ISBN of book '{bookName}' is empty.");
            ReportLog.Info($"Book '{bookName}' is found with ISBN '{book.Isbn}'.");
            
            return book.Isbn;
        }
    }
}