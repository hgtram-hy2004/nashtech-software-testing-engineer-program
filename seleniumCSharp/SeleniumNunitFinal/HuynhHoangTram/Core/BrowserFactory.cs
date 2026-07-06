using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace HuynhHoangTram.Core
{
    public class BrowserFactory
    {
        private static readonly ThreadLocal<IWebDriver?> ThreadLocalWebDriver =
            new ThreadLocal<IWebDriver?>();

        private static readonly object DriverSetupLock = new object();

        public static void InitializeDriver(string browserName)
        {
            if (ThreadLocalWebDriver.Value != null)
            {
                return;
            }

            switch (browserName.ToLower())
            {
                case "chrome":
                    lock (DriverSetupLock)
                    {
                        new DriverManager().SetUpDriver(
                            new ChromeConfig(),
                            VersionResolveStrategy.MatchingBrowser
                        );
                    }

                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("test-type");
                    chromeOptions.AddArguments("--no-sandbox");
                    chromeOptions.AddArguments("--disable-dev-shm-usage");
                    chromeOptions.AddArguments("--disable-notifications");
                    chromeOptions.AddArguments("--disable-popup-blocking");

                    ThreadLocalWebDriver.Value = new ChromeDriver(chromeOptions);
                    break;

                case "firefox":
                    lock (DriverSetupLock)
                    {
                        new DriverManager().SetUpDriver(
                            new FirefoxConfig(),
                            VersionResolveStrategy.MatchingBrowser
                        );
                    }

                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArguments("--no-sandbox");

                    ThreadLocalWebDriver.Value = new FirefoxDriver(firefoxOptions);
                    break;

                default:
                    throw new ArgumentException("Not a valid driver: " + browserName);
            }
        }

        public static IWebDriver GetWebDriver()
        {
            return ThreadLocalWebDriver.Value
                ?? throw new Exception("WebDriver has not been initialized for this thread.");
        }

        public static void QuitBrowser()
        {
            IWebDriver? driver = ThreadLocalWebDriver.Value;

            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();

                ThreadLocalWebDriver.Value = null;
            }
        }
    }
}