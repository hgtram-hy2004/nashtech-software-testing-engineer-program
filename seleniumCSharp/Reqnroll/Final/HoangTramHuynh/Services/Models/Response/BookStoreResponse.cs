using Newtonsoft.Json;

namespace HoangTramHuynh.Models.Response
{
    public class BookStoreResponse
    {
        [JsonProperty("books")]
        public List<BookStoreBookResponse> Books { get; set; } = new();
    }

    public class BookStoreBookResponse
    {
        [JsonProperty("isbn")]
        public string Isbn { get; set; } = string.Empty;

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
    }
}