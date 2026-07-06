using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoangTramHuynh.Core.UI;
using OpenQA.Selenium;

namespace HoangTramHuynh.Utils
{
    public class ScreenshotUtils
    {
        public static string CaptureScreenshot(string screenshotName)
        {
            string reportFolder = PathUtils.GetExtentReportFolderPath();
            string screenshotFolder = Path.Combine(reportFolder,"Screenshots");

            Directory.CreateDirectory(screenshotFolder);

            string fileName =screenshotName + "_" +DateTime.Now.ToString("yyyyMMdd_HHmmss") +".png";
            string screenshotPath = Path.Combine(screenshotFolder,fileName);
            ITakesScreenshot takesScreenshot =(ITakesScreenshot)BrowserFactory.GetWebDriver();
            Screenshot screenshot = takesScreenshot.GetScreenshot();
            screenshot.SaveAsFile(screenshotPath);
            return screenshotPath;
        }
    }
}