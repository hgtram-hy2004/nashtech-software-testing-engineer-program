using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace HuynhHoangTram.Core.Utilities
{
    public class ConfigUtils
    {
        private static readonly string AppSettingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Configuration","appsetting.json");
        private static readonly JObject Configuration = JObject.Parse(File.ReadAllText(AppSettingsFilePath));
        public static string GetConfigurationByKey(string key)
        {
            var keys = key.Split(":");
            JToken? currentToken = Configuration;
            foreach (var item in keys)
            {
                currentToken = currentToken?[item];
            }
            return currentToken?.ToString() ?? throw new Exception($"Configuration key '{key}' was not found.");
        }
    }
}