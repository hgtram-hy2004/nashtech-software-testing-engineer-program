using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace HuynhHoangTram.Models.Request
{
    public class AccountRequest
    {
        [JsonProperty("userName")]
        public string userName { get; set; } = string.Empty;

        [JsonProperty("password")]
        public string password { get; set; } = string.Empty;

    }
}