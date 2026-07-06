using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuynhHoangTram.Utils
{
    public class FileUtils
    {
         public static string GetAbsolutePath(string relativePath)
        {
            return Path.GetFullPath(relativePath);
        }

        public static string GetCurrentDirectoryPath(params string[] paths)
        {
            return Path.Combine(
                new[] { Directory.GetCurrentDirectory() }
                    .Concat(paths)
                    .ToArray()
            );
        }

        public static void CreateDirectoryIfNotExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}