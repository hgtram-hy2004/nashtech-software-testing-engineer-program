using HoangTramHuynh.Core.UI;
using HoangTramHuynh.Utils;
using HoangTramHuynh.Core.Report;
using Reqnroll;

namespace HoangTramHuynh.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ConfigurationUtils.ReadConfiguration("Configurations\\appsettings.json");
            ExtentReport.InitReport();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            ExtentReport.CreateTest(_scenarioContext.ScenarioInfo.Title);

            ReportLog.Info($"Start scenario: {_scenarioContext.ScenarioInfo.Title}");

            BrowserFactory.InitializeDriver(ConfigurationUtils.GetConfigurationByKey("Browser"));
            DriverUtils.MaximizeWindow();
            DriverUtils.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestUrl"));

            ReportLog.Info("Browser is initialized and navigated to test URL.");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TestError == null)
            {
                ExtentReport.LogPass($"Scenario passed: {_scenarioContext.ScenarioInfo.Title}");
            }
            else
            {
                ExtentReport.LogFail($"Scenario failed: {_scenarioContext.ScenarioInfo.Title}");
                ExtentReport.LogFail(_scenarioContext.TestError.Message);
                string screenshotPath = ScreenshotUtils.CaptureScreenshot(_scenarioContext.ScenarioInfo.Title);
                ExtentReport.Test.AddScreenCaptureFromPath(screenshotPath);
            }
            BrowserFactory.QuitBrowser();
            ExtentReport.ClearTest();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            TestContext.Progress.WriteLine("Finish test execution");
            ExtentReport.FlushReport();
        }
    }
}