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

namespace HoangTramHuynh.Core.UI
{
    public class BrowserFactory
    {
        private static readonly AsyncLocal<IWebDriver?> _driver = new();
        private static readonly object DriverSetupLock = new object();
        public static void InitializeDriver(string browserName)
        {
            if (_driver.Value != null)
            {
                return;
            }
            switch (browserName.ToLower())
            {
                case "chrome":
                    lock (DriverSetupLock)
                    {
                        new DriverManager().SetUpDriver( new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    }
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("test-type");
                    chromeOptions.AddArguments("--no-sandbox");
                    chromeOptions.AddArguments("--disable-dev-shm-usage");
                    chromeOptions.AddArguments("--disable-notifications");
                    chromeOptions.AddArguments("--disable-popup-blocking");
                    chromeOptions.AddUserProfilePreference("profile.default_zoom_level", -2.19401891384275);


                    _driver.Value = new ChromeDriver(chromeOptions);
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

                    _driver.Value = new FirefoxDriver(firefoxOptions);
                    break;

                default:
                    throw new ArgumentException("Not a valid driver: " + browserName);
            }
        }

        public static IWebDriver GetWebDriver()
        {
            return _driver.Value ?? throw new Exception("WebDriver has not been initialized for this thread.");
        }

        public static void QuitBrowser()
        {
            IWebDriver? driver = _driver.Value;

            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                _driver.Value = null;
            }
        }
    }
}