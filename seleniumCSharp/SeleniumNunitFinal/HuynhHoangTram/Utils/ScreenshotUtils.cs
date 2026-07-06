using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuynhHoangTram.Core;
using OpenQA.Selenium;

namespace HuynhHoangTram.Utils
{
    public class ScreenshotUtils
    {
        public static string CaptureScreenshot(string screenshotName)
        {
            string projectRoot = Path.GetFullPath(
               Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..")
           );

            string testResultsFolder = Path.Combine(
                projectRoot,
                "TestResults"
            );

            Directory.CreateDirectory(testResultsFolder);

            string fileName =
                screenshotName + "_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                ".png";

            string screenshotPath = Path.Combine(
                testResultsFolder,
                fileName
            );

            ITakesScreenshot takesScreenshot =
                (ITakesScreenshot)BrowserFactory.GetWebDriver();

            Screenshot screenshot = takesScreenshot.GetScreenshot();

            screenshot.SaveAsFile(screenshotPath);

            return screenshotPath;
        }
    }
}