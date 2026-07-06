using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HuynhHoangTram.Models.Response
{
    public class AccountResponse
    {
        [JsonProperty("userId")]
        public string UserId { get; set; } = string.Empty;

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("books")]
        public List<BookResponse> Books { get; set; } = new();
        [JsonProperty("expectedBooks")]
        public List<BookResponse> ExpectedBooks { get; set; } = new();
    }
    public class BookResponse
    {
        [JsonProperty("isbn")]
        public string Isbn { get; set; } = string.Empty;

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
    }
}