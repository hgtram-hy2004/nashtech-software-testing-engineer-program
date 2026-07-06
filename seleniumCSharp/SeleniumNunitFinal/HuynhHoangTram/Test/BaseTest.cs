using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using HuynhHoangTram.Core;
using HuynhHoangTram.Utils;
using HuynhHoangTram.Report;

namespace HuynhHoangTram.Test
{
    public class BaseTest
    {
        protected IWebDriver Driver
        {
            get { return BrowserFactory.GetWebDriver(); }
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ConfigurationUtils.ReadConfiguration("Configurations\\appsettings.json");

            string projectRoot = Path.GetFullPath(
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..")
            );

            string reportPath = Path.Combine(
                projectRoot,
                "TestResults",
                "index.html"
            );

            ExtentReportHelpers.InitializeReport(
                reportPath,
                "QA",
                Environment.MachineName,
                ConfigurationUtils.GetConfigurationByKey("Browser")
            );

            ExtentReportHelpers.CreateProject("HuynhHoangTram");
        }

        [SetUp]
        public void SetUp()
        {
            BrowserFactory.InitializeDriver(ConfigurationUtils.GetConfigurationByKey("Browser"));
            DriverUtils.MaximizeWindow();
            DriverUtils.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestUrl"));

            ExtentReportHelpers.CreateTestCase(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            TestStatus status =
         TestContext.CurrentContext.Result.Outcome.Status;

            string stackTrace =
                string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : TestContext.CurrentContext.Result.StackTrace;

            ExtentReportHelpers.CreateTestResult(
                status,
                stackTrace,
                TestContext.CurrentContext.Test.ClassName ?? "Test Class",
                TestContext.CurrentContext.Test.Name
            );

            BrowserFactory.QuitBrowser();

            ExtentReportHelpers.Flush();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportHelpers.Flush();
            TestContext.Progress.WriteLine("Finish test execution");
        }
    }
}