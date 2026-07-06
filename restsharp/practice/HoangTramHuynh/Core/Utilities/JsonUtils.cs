using Newtonsoft.Json;

namespace HoangTramHuynh.Core.Utilities
{
    public static class JsonUtils
    {
        public static T ReadJsonFile<T>(string filePath)
        {
            var jsonContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonContent)?? throw new Exception($"Cannot deserialize JSON file: {filePath}");
        }
    }
}