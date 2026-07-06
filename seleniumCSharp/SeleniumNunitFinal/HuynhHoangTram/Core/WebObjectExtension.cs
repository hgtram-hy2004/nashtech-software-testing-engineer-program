using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using HuynhHoangTram.Utils;
namespace HuynhHoangTram.Core
{
    public static class WebObjectExtension
    {
        public static WebDriverWait GetWait()
        {
            int timeoutInSeconds = int.Parse(ConfigurationUtils.GetConfigurationByKey("WebDriver.Wait.Time"));

            WebDriverWait wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(timeoutInSeconds));

            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));

            return wait;
        }

        public static IWebElement WaitForElementToBeVisible(this WebObject webObject)
        {
            return GetWait().Until(driver =>
            {
                IWebElement element = driver.FindElement(webObject.By);

                return element.Displayed
                    ? element
                    : null;
            });
        }

        public static IWebElement WaitForElementToBeClickable(this WebObject webObject)
        {
            return GetWait().Until(driver =>
            {
                IWebElement element = driver.FindElement(webObject.By);

                return element.Displayed && element.Enabled
                    ? element
                    : null;
            });
        }
        public static IWebElement WaitForElementToExist(this WebObject webObject)
        {
            return GetWait().Until(driver =>
                driver.FindElement(webObject.By)
            );
        }
        public static bool WaitUntil(
     Func<IWebDriver, bool> condition,
     int? timeoutInSeconds = null
 )
        {
            try
            {
                WebDriverWait wait = timeoutInSeconds == null
                    ? GetWait()
                    : new WebDriverWait(
                        BrowserFactory.GetWebDriver(),
                        TimeSpan.FromSeconds(timeoutInSeconds.Value)
                    );

                wait.IgnoreExceptionTypes(
                    typeof(StaleElementReferenceException),
                    typeof(NoSuchElementException)
                );

                wait.Until(condition);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static IAlert WaitForAlert()
        {
            return GetWait().Until(driver =>
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            });
        }
        public static bool IsElementDisplayed(this WebObject webObject)
        {
            return WaitForElementToBeVisible(webObject).Displayed;
        }

        public static void ScrollToElement(this WebObject webObject, int offsetY = 0)
        {
            IWebElement element = webObject.WaitForElementToBeVisible();

            IJavaScriptExecutor js =
                (IJavaScriptExecutor)BrowserFactory.GetWebDriver();

            js.ExecuteScript(
                "arguments[0].scrollIntoView({block: 'center', inline: 'nearest'});",
                element
            );

            if (offsetY != 0)
            {
                js.ExecuteScript($"window.scrollBy(0, {offsetY});");
            }
        }

        public static void ClickOnElement(this WebObject webObject)
        {
            //webObject.ScrollToElement();

            IWebElement element = webObject.WaitForElementToBeClickable();

            element.Click();
        }
        public static void ClickDropdownOptionByMouse(this WebObject dropdown, WebObject option)
        {
            IWebElement dropdownElement = dropdown.WaitForElementToBeClickable();
            dropdownElement.Click();

            IWebElement optionElement = option.WaitForElementToExist();
            optionElement.Click();
        }
        public static void EnterText(this WebObject webObject, string text)
        {
            var element = WaitForElementToBeVisible(webObject);

            element.Clear();

            element.SendKeys(text);
        }

        public static string GetText(this WebObject webObject)
        {
            return WaitForElementToBeVisible(webObject).Text;
        }
        public static void SelectByText(this WebObject webObject, string text)
        {
            IWebElement element = (IWebElement)webObject.WaitForElementToBeVisible();

            SelectElement select = new SelectElement(element);

            select.SelectByText(text);
        }
        public static string GetAttributeValue(this WebObject webObject, string attributeName)
        {
            IWebElement element = (IWebElement)webObject.WaitForElementToBeVisible();
            return element.GetAttribute(attributeName);
        }
        public static string NormalizeText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "";
            }
            return text.Trim()
            .Replace("\r", "")
            .Replace("\n", " ")
            .Replace("  ", " ");
        }

        public static bool IsTextEqual(string actual, string expected)
        {
            return NormalizeText(actual).Equals(
                NormalizeText(expected),
                StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsTextContains(string actual, string expected)
        {
            return NormalizeText(actual).Contains(
            NormalizeText(expected),
            StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsOptionsMatched(string actual, string[] expectedOptions)
        {
            string normalizedActual = NormalizeText(actual);

            return expectedOptions.All(option =>
                normalizedActual.Contains(
                NormalizeText(option),
                StringComparison.OrdinalIgnoreCase));
        }

    }

}