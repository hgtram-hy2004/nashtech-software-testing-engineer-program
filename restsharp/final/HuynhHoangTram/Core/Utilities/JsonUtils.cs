using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace HuynhHoangTram.Core.Utilities
{
    public class JsonUtils
    {
         public static T ReadJsonFile<T>(string filePath)
        {
            var jsonContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonContent)?? throw new Exception($"Cannot deserialize JSON file: {filePath}");
        }
    }
}