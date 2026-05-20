using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace HuynhHoangTram.Core;

public static class WebObjectExtension
{
    private static WebDriverWait GetWait()
    {
        WebDriverWait wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(30));
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
    public static bool WaitUntilAnyElementDisplayed(params WebObject[] webObjects)
    {
        return GetWait().Until(driver =>
        {
            foreach (WebObject webObject in webObjects)
            {
                bool isDisplayed = driver
                    .FindElements(webObject.By)
                    .Any(element => element.Displayed);

                if (isDisplayed)
                {
                    return true;
                }
            }

                return false;
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

    public static bool IsElementDisplayed(this WebObject webObject)
    {
        return webObject.WaitForElementToBeVisible().Displayed;
    }

    public static void ClickOnElement(this WebObject webObject)
    {
        var element = webObject.WaitForElementToBeClickable();
        element.Click();
    }

    public static void EnterText(this WebObject webObject, string text)
    {
        var element = webObject.WaitForElementToBeVisible();
        element.Clear();
        element.SendKeys(text);
    }

    public static string GetText(this WebObject webObject)
    {
        return webObject.WaitForElementToBeVisible().Text;
    }
    public static void SetTextByJavaScript(this WebObject webObject, string text)
    {
        IWebElement element = webObject.WaitForElementToBeVisible();
        IJavaScriptExecutor js = (IJavaScriptExecutor)BrowserFactory.GetWebDriver();
        js.ExecuteScript("arguments[0].removeAttribute('readonly'); arguments[0].value = arguments[1]; arguments[0].dispatchEvent(new Event('input')); arguments[0].dispatchEvent(new Event('change'));",
            element,
            text
        );
    }
    public static int GetColumnIndex(this WebObject tableObject, string columnName)
    {
        IWebElement table = tableObject.WaitForElementToBeVisible();
        List<IWebElement> headers = table.FindElements(By.XPath(".//thead//th")).ToList();

        for (int i = 0; i < headers.Count; i++)
        {
            string actualHeader = NormalizeText(headers[i].Text);
            if (actualHeader.Equals(columnName, StringComparison.OrdinalIgnoreCase))
            {
                return i + 1;
            }
        }

        throw new Exception($"Column '{columnName}' was not found in table '{tableObject.Name}'.");
    }

    public static List<IWebElement> GetRows(this WebObject tableObject)
    {
        IWebElement table = tableObject.WaitForElementToBeVisible();
        return table.FindElements(By.XPath(".//tbody//tr")).Where(row => row.Displayed).ToList();
    }

    public static string GetCellValue(this WebObject tableObject, IWebElement row, string columnName)
    {
        int columnIndex = tableObject.GetColumnIndex(columnName);
        IWebElement cell = row.FindElement(By.XPath($"./td[{columnIndex}]"));
        return NormalizeText(cell.Text);
    }

    public static bool IsTableHasRows(this WebObject tableObject)
    {
        return tableObject.GetRows().Count > 0;
    }

    private static string NormalizeText(string text)
    {
        return text
            .Trim()
            .Replace("\r", "")
            .Replace("\n", " ")
            .Replace("  ", " ");
    }
    public static string SelectRandomOption(this WebObject webObject)
    {
        IWebElement element = webObject.WaitForElementToBeVisible();
        SelectElement select = new SelectElement(element);
        List<IWebElement> options = select.Options
            .Where(option =>
                !string.IsNullOrWhiteSpace(option.Text)
                && !option.Text.Trim().Equals("All", StringComparison.OrdinalIgnoreCase)
                && !option.Text.ToLower().Contains("please select")
            ).ToList();
        if (options.Count == 0)
        {
            throw new Exception($"No valid option found for {webObject.Name}");
        }
        Random random = new Random();
        IWebElement selectedOption = options[random.Next(options.Count)];
        select.SelectByText(selectedOption.Text);
        return selectedOption.Text.Trim();
    }
}
