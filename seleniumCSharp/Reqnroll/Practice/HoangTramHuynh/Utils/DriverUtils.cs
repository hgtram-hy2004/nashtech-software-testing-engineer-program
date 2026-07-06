using System;
using OpenQA.Selenium;
using HoangTramHuynh.Core;
namespace HoangTramHuynh.Utils;

public class DriverUtils
{
    public static IWebDriver GetDriver()
    {
        return BrowserFactory.ThreadLocalWebDriver.Value;
    }

    public static void GoToUrl(string url)
    {
        GetDriver().Navigate().GoToUrl(url);
    }

    public static void MaximizeWindow()
    {
        GetDriver().Manage().Window.Maximize();
    }
}
