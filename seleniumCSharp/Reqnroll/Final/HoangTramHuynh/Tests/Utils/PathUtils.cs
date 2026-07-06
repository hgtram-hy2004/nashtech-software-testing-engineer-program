using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoangTramHuynh.Utils
{
    public class PathUtils
    {
        public static string GetProjectRootDirectory()
        {
            var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (currentDirectory != null && !currentDirectory.GetFiles("*.csproj").Any())
            {
                currentDirectory = currentDirectory.Parent;
            }

            if (currentDirectory == null)
            {
                throw new DirectoryNotFoundException("Project root directory was not found.");
            }
            return currentDirectory.FullName;
        }

        public static string GetTestDataFilePath(string fileName)
        {
            return Path.Combine(
                GetProjectRootDirectory(),
                "Test",
                "Data",
                fileName
            );
        }

        public static string GetSchemaFilePath(string fileName)
        {
            return Path.Combine(
                GetProjectRootDirectory(),
                "Test",
                "Data",
                "Schemas",
                fileName
            );
        }
       public static string GetExtentReportFolderPath()
        {
            return Path.Combine(
                AppContext.BaseDirectory,
                "TestResults",
                "ExtentReports"
            );
        }
    }
}