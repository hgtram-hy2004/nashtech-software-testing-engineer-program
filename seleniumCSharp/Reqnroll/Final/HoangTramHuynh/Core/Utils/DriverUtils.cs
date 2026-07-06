using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HoangTramHuynh.Core.UI;
using OpenQA.Selenium.Interactions;


namespace HoangTramHuynh.Utils
{
    public class DriverUtils
    {
        public static IWebDriver GetDriver()
        {
            return HoangTramHuynh.Core.UI.BrowserFactory.GetWebDriver();
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
}