using System.Collections.Concurrent;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using HuynhHoangTram.Core.Utilities;
using NUnit.Framework;

namespace HuynhHoangTram.Core.Report
{
    public class ExtentReport
    {
        private static ExtentReports _extent = null!;
        private static readonly ConcurrentDictionary<string, ExtentTest> _tests = new();
        private static readonly object _lock = new();
        private static bool _isInitialized = false;

        public static ExtentTest Test
        {
            get
            {
                var testId = TestContext.CurrentContext.Test.ID;

                if (_tests.TryGetValue(testId, out var test))
                {
                    return test;
                }

                throw new InvalidOperationException(
                    $"ExtentTest has not been created for test id: {testId}. Please call ExtentReport.CreateTest() in [SetUp] before logging."
                );
            }
        }

        public static void InitReport()
        {
            lock (_lock)
            {
                if (_isInitialized)
                {
                    return;
                }

                var reportFolder = PathUtils.GetExtentReportFolderPath();

                Directory.CreateDirectory(reportFolder);

                var reportPath = Path.Combine(
                    reportFolder,
                    $"API_Test_Report_{DateTime.Now:yyyyMMdd_HHmmss}.html"
                );

                var sparkReporter = new ExtentSparkReporter(reportPath);

                _extent = new ExtentReports();
                _extent.AttachReporter(sparkReporter);

                _extent.AddSystemInfo("Project", "API Testing With RestSharp");
                _extent.AddSystemInfo("Framework", "NUnit");
                _extent.AddSystemInfo("Language", "C#");

                _isInitialized = true;
            }
        }

        public static void CreateTest(string testName)
        {
            if (!_isInitialized || _extent == null)
            {
                InitReport();
            }

            var testId = TestContext.CurrentContext.Test.ID;
            var test = _extent.CreateTest(testName);

            _tests[testId] = test;
        }

        public static void LogInfo(string message)
        {
            Test.Info(message);
        }

        public static void LogPass(string message)
        {
            Test.Pass(message);
        }

        public static void LogFail(string message)
        {
            Test.Fail(message);
        }

        public static void FlushReport()
        {
            lock (_lock)
            {
                _extent?.Flush();
            }
        }

        public static void ClearTest()
        {
            var testId = TestContext.CurrentContext.Test.ID;
            _tests.TryRemove(testId, out _);
        }
    }
}