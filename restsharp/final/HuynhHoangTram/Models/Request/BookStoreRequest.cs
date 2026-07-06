using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HuynhHoangTram.Models.Request
{
    public class BookStoreRequest
    {
        [JsonProperty("userId")]
        public string UserId { get; set; } = string.Empty;

        [JsonProperty("isbn", NullValueHandling = NullValueHandling.Ignore)]
        public string? Isbn { get; set; }

        [JsonProperty("collectionOfIsbns", NullValueHandling = NullValueHandling.Ignore)]
        public List<IsbnDto>? CollectionOfIsbns { get; set; }
    }

    public class IsbnDto
    {
        [JsonProperty("isbn")]
        public string Isbn { get; set; } = string.Empty;
    }
}