using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HuynhHoangTram.Models.Response
{
    public class GenerateTokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;
    }
}