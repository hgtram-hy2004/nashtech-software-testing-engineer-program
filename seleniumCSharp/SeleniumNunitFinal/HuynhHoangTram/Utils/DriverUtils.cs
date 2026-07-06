using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HuynhHoangTram.Core;

namespace HuynhHoangTram.Utils
{
    public class DriverUtils
    {
        public static IWebDriver GetDriver()
        {
            return HuynhHoangTram.Core.BrowserFactory.GetWebDriver();
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