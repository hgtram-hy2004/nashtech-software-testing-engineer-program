using Newtonsoft.Json;

namespace HoangTramHuynh.Models.Response
{
    public class AccountResponse
    {
        [JsonProperty("userId")]
        public string UserId { get; set; } = string.Empty;

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;

        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;

        [JsonProperty("books")]
        public List<BookResponse> Books { get; set; } = new();
    }

    public class BookResponse
    {
        [JsonProperty("isbn")]
        public string Isbn { get; set; } = string.Empty;

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
    }
}