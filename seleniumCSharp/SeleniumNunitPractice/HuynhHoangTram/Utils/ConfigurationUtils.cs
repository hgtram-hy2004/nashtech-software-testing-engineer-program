using System;
using Microsoft.Extensions.Configuration;
namespace HuynhHoangTram.Utils;

public class ConfigurationUtils
{ 
    private static IConfigurationRoot configuration = null!;

        public static void ReadConfiguration(string jsonFilePath)
        {
            string projectRoot = GetProjectRootDirectory();

            configuration = new ConfigurationBuilder()
                .SetBasePath(projectRoot)
                .AddJsonFile(
                    jsonFilePath,
                    optional: false,
                    reloadOnChange: true
                )
                .Build();
        }

        public static string GetConfigurationByKey(string key)
        {
            return configuration[key]
                ?? throw new Exception(
                    $"Configuration key '{key}' was not found."
                );
        }

        private static string GetProjectRootDirectory()
        {
            DirectoryInfo directory =
                new DirectoryInfo(AppContext.BaseDirectory);

            while (directory != null)
            {
                bool hasProjectFile =
                    directory.GetFiles("*.csproj").Any();

                bool hasConfigurationsFolder =
                    directory.GetDirectories("Configurations").Any();

                if (hasProjectFile && hasConfigurationsFolder)
                {
                    return directory.FullName;
                }

                directory = directory.Parent!;
            }

            throw new Exception(
                "Project root directory was not found."
            );
        }
}
