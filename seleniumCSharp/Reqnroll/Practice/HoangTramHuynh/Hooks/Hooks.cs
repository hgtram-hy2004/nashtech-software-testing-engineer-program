using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoangTramHuynh.Core;
using HoangTramHuynh.Utils;
using Reqnroll;


namespace HoangTramHuynh.Hooks
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            ConfigurationUtils.ReadConfiguration("Configurations\\appsettings.json");
            BrowserFactory.InitializeDriver(ConfigurationUtils.GetConfigurationByKey("Browser"));
            DriverUtils.MaximizeWindow();
            DriverUtils.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestUrl"));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            BrowserFactory.QuitDriver();
        }
    }
}