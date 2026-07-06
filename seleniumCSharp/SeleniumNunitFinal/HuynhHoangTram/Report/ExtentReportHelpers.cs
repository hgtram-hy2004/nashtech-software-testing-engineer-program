using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using HuynhHoangTram.Core;
using HuynhHoangTram.Utils;

namespace HuynhHoangTram.Report
{
    public class ExtentReportHelpers
    {
        public static ExtentReports ExtentManager = null!;

        private static readonly object LockObject = new object();

        public static ExtentTest ProjectTest = null!;

        [ThreadStatic]
        public static ExtentTest TestCaseNode = null!;

        [ThreadStatic]
        public static ExtentTest StepNode = null!;

        public static void InitializeReport( string reportPath, string hostName, string environment,string browser)
        {
            lock (LockObject)
            {
                if (ExtentManager != null)
                {
                    return;
                }

                ExtentSparkReporter sparkReporter = new ExtentSparkReporter(reportPath);

                ExtentManager = new ExtentReports();

                ExtentManager.AttachReporter(sparkReporter);

                ExtentManager.AddSystemInfo("Host Name", hostName);
                ExtentManager.AddSystemInfo("Environment", environment);
                ExtentManager.AddSystemInfo("Browser", browser);
            }
        }

        public static void CreateProject(string projectName)
        {
            lock (LockObject)
            {
                if (ProjectTest != null)
                {
                    return;
                }

                ProjectTest = ExtentManager.CreateTest(projectName);
            }
        }

        public static void CreateTestCase(string testCaseName)
        {
            if (ProjectTest == null)
            {
                throw new Exception("ProjectTest has not been initialized.");
            }

            TestCaseNode = ProjectTest.CreateNode(testCaseName);
        }

        public static void ExecuteStep(string stepName, Action stepAction)
        {
            try
            {
                StepNode = TestCaseNode.CreateNode(stepName);

                stepAction();

                StepNode.Pass(stepName);
            }
            catch (Exception ex)
            {
                string screenshotPath = ScreenshotUtils.CaptureScreenshot(
                    stepName.Replace(" ", "_").Replace(":", "")
                );

                StepNode.Fail(stepName);
                StepNode.Fail(ex.Message);
                StepNode.AddScreenCaptureFromPath(screenshotPath);

                throw;
            }
        }

        public static void CreateTestResult(
            NUnit.Framework.Interfaces.TestStatus status,
            string? stacktrace,
            string? className,
            string testName
        )
        {
            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                TestCaseNode.Fail("Test failed: " + testName);

                if (!string.IsNullOrWhiteSpace(stacktrace))
                {
                    TestCaseNode.Fail(stacktrace);
                }
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Skipped)
            {
                TestCaseNode.Skip("Test skipped: " + testName);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Inconclusive)
            {
                TestCaseNode.Warning("Test inconclusive: " + testName);
            }
            else
            {
                TestCaseNode.Pass("Test passed: " + testName);
            }
        }

        public static void Flush()
        {
            lock (LockObject)
            {
                ExtentManager.Flush();
            }
        }
    }
}