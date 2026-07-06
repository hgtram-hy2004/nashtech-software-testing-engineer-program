using System.Text;
using AventStack.ExtentReports.MarkupUtils;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HoangTramHuynh.Core.Report
{
    public class ReportLog
    {
        public static void Info(string message)
        {
            ExtentReport.LogInfo(message);
        }

        public static void LogResponse(string apiName, RestResponse response, long responseTime)
        {
            var report = new StringBuilder();
            report.AppendLine($"API Name: {apiName}");
            report.AppendLine($"Status Code: {(int)response.StatusCode} {response.StatusCode}");
            report.AppendLine($"Response Time: {responseTime} ms");
            ExtentReport.LogInfo($"<b>{apiName}</b>");
            ExtentReport.LogInfo($"Status Code: {(int)response.StatusCode} {response.StatusCode}");
            ExtentReport.LogInfo($"Response Time: {responseTime} ms");
            LogResponseBodyAsExtentTable(response.Content);
        }

        private static void LogResponseBodyAsExtentTable(string? responseContent)
        {
            if (string.IsNullOrWhiteSpace(responseContent))
            {
                ExtentReport.LogInfo("Response Body: Empty");
                return;
            }
            var token = JToken.Parse(responseContent);
            if (token is JArray jsonArray)
            {
                LogJsonArrayAsExtentTable(jsonArray);
                return;
            }
            if (token is JObject jsonObject)
            {
                LogJsonObjectAsExtentTable(jsonObject);
                return;
            }
            ExtentReport.LogInfo($"Response Body: {responseContent}");
        }

        private static void LogJsonArrayAsExtentTable(JArray jsonArray)
        {
            if (!jsonArray.Any())
            {
                ExtentReport.LogInfo("Response Body: []");
                return;
            }
            var table = new List<string[]>
            {
                new[] { "Id", "Name", "Email", "Gender", "Status" }
            };
            foreach (var item in jsonArray)
            {
                table.Add(new[]
                {
                    item["id"]?.ToString() ?? string.Empty,
                    item["name"]?.ToString() ?? string.Empty,
                    item["email"]?.ToString() ?? string.Empty,
                    item["gender"]?.ToString() ?? string.Empty,
                    item["status"]?.ToString() ?? string.Empty
                });
            }
            ExtentReport.Test.Info(MarkupHelper.CreateTable(ConvertToTwoDimensionalArray(table)));
        }

        private static void LogJsonObjectAsExtentTable(JObject jsonObject)
        {
            var table = new List<string[]>
            {
                new[] { "Id", "Name", "Email", "Gender", "Status" },
                new[]
                {
                    jsonObject["id"]?.ToString() ?? string.Empty,
                    jsonObject["name"]?.ToString() ?? string.Empty,
                    jsonObject["email"]?.ToString() ?? string.Empty,
                    jsonObject["gender"]?.ToString() ?? string.Empty,
                    jsonObject["status"]?.ToString() ?? string.Empty
                }
            };
            ExtentReport.Test.Info(MarkupHelper.CreateTable(ConvertToTwoDimensionalArray(table)));
        }
        private static string[,] ConvertToTwoDimensionalArray(List<string[]> rows)
        {
            var rowCount = rows.Count;
            var columnCount = rows[0].Length;
            var result = new string[rowCount, columnCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    result[row, column] = rows[row][column];
                }
            }
            return result;
        }
    }
}