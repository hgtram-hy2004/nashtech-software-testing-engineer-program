using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using HoangTramHuynh.Core.Utilities;

namespace HoangTramHuynh.Core.Report
{
    public static class ExtentReport
    {
        private static ExtentReports _extent = null!;
        private static readonly AsyncLocal<ExtentTest?> _test = new();
        private static readonly object _lock = new();
        private static bool _isInitialized = false;

        public static ExtentTest Test
        {
            get
            {
                if (_test.Value == null)
                {
                    throw new InvalidOperationException("ExtentTest has not been created. Please call ExtentReport.CreateTest() in [SetUp] before logging.");
                }
                return _test.Value;
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
            if (_extent == null)
            {
                InitReport();
            }
            _test.Value = _extent.CreateTest(testName);
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

    }
}