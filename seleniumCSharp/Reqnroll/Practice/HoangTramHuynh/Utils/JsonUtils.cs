using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HoangTramHuynh.Utils
{
    public class JsonUtils
    {
         public static T ReadJsonFile<T>(string filePath)
        {
            string fullPath = Path.Combine(AppContext.BaseDirectory,filePath);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("Json file was not found: " + fullPath);
            }

            string jsonContent = File.ReadAllText(fullPath);
            T? data = JsonConvert.DeserializeObject<T>(jsonContent);
            if (data == null)
            {
                throw new Exception("Cannot deserialize json file: " + fullPath);
            }

            return data;
        }
    }
}