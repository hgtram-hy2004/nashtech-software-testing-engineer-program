using System;
using NUnit.Framework;
using OpenQA.Selenium;
using HuynhHoangTram.Core;
using HuynhHoangTram.Utils;
namespace HuynhHoangTram.Test;

public class BaseTest
{
    protected IWebDriver Driver
    {
        get{return BrowserFactory.GetWebDriver();}
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        ConfigurationUtils.ReadConfiguration("Configurations\\appsettings.json");
    }

    [SetUp]
    public void SetUp()
    {
        BrowserFactory.InitializeDriver(ConfigurationUtils.GetConfigurationByKey("Browser"));

        DriverUtils.MaximizeWindow();

        DriverUtils.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestUrl"));
    }

    [TearDown]
    public void TearDown()
    {
        IWebDriver driver = BrowserFactory.GetWebDriver();

        if (driver != null)
        {
            driver.Quit();
            driver.Dispose();
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        TestContext.Progress.WriteLine("Finish test execution");
    }
}
