using System.Text;
using AventStack.ExtentReports.MarkupUtils;
using HuynhHoangTram.Models.Response;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HuynhHoangTram.Core.Report
{
    public class ReportLog
    {
        public static void Info(string message)
        {
            ExtentReport.LogInfo(message);
        }

        public static void LogResponse(string apiName, RestResponse response, long responseTime)
        {
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

            try
            {
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
            catch
            {
                ExtentReport.LogInfo($"Response Body: {responseContent}");
            }
        }

        private static void LogJsonObjectAsExtentTable(JObject jsonObject)
        {
            var table = new List<string[]>
            {
                new[] { "Field", "Value" }
            };

            foreach (var property in jsonObject.Properties())
            {
                table.Add(new[]
                {
                    property.Name,
                    FormatJsonValue(property.Value)
                });
            }

            ExtentReport.Test.Info(MarkupHelper.CreateTable(ConvertToTwoDimensionalArray(table)));
        }

        private static void LogJsonArrayAsExtentTable(JArray jsonArray)
        {
            if (!jsonArray.Any())
            {
                ExtentReport.LogInfo("Response Body: []");
                return;
            }

            var firstObject = jsonArray.First as JObject;

            if (firstObject == null)
            {
                ExtentReport.LogInfo(jsonArray.ToString());
                return;
            }

            var headers = firstObject.Properties()
                .Select(property => property.Name)
                .ToList();

            var table = new List<string[]>
            {
                headers.ToArray()
            };

            foreach (var item in jsonArray)
            {
                if (item is not JObject jsonObject)
                {
                    continue;
                }

                var row = headers
                    .Select(header => FormatJsonValue(jsonObject[header]))
                    .ToArray();

                table.Add(row);
            }

            ExtentReport.Test.Info(MarkupHelper.CreateTable(ConvertToTwoDimensionalArray(table)));
        }
        public static void LogBooks(List<BookResponse>? books)
        {
            if (books == null || !books.Any())
            {
                ExtentReport.LogInfo("Books verification passed: This account does not have any books in collection.");
                return;
            }

            ExtentReport.LogInfo($"Books verification passed. Total books: {books.Count}");

            var table = new List<string[]>
            {
                new[] { "No.", "ISBN", "Title" }
            };

            for (int i = 0; i < books.Count; i++)
            {
                table.Add(new[]
                {
                    (i + 1).ToString(),
                    books[i].Isbn,
                    books[i].Title
                });
            }

            ExtentReport.Test.Info(MarkupHelper.CreateTable(ConvertToTwoDimensionalArray(table)));
        }

        private static string FormatJsonValue(JToken? value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is JArray array)
            {
                return $"Array Count: {array.Count}";
            }

            if (value is JObject)
            {
                return value.ToString(Newtonsoft.Json.Formatting.None);
            }

            return value.ToString();
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