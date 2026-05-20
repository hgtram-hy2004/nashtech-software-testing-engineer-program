using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
namespace HuynhHoangTram.Core;

public class BrowserFactory
{
    public static ThreadLocal<IWebDriver> ThreadLocalWebDriver = new ThreadLocal<IWebDriver>();

    public static void InitializeDriver(string browserName)
    {
        switch (browserName.ToLower())
        {
            case "chrome":

                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("test-type");
                chromeOptions.AddArguments("--no-sandbox");
                ThreadLocalWebDriver.Value = new ChromeDriver(chromeOptions);
                break;

            case "firefox":

                new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);
                var firefoxOptions = new FirefoxOptions();
                firefoxOptions.AddArguments("--no-sandbox");
                ThreadLocalWebDriver.Value = new FirefoxDriver(firefoxOptions);
                break;

            default:
                throw new ArgumentException("Not a valid driver");
        }
    }

    public static IWebDriver GetWebDriver()
    {
        return ThreadLocalWebDriver.Value!;
    }
}
