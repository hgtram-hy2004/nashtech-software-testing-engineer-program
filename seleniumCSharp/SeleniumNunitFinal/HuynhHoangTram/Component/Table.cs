using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HuynhHoangTram.Core;

namespace HuynhHoangTram.Component
{
    public class Table : WebObject
    {
        public Table(By by, string name = "") : base(by, name)
        {
        }
        public int GetColumnIndex(string columnName)
        {
            IWebElement table = (IWebElement)this.WaitForElementToBeVisible();
            List<IWebElement> headers = table.FindElements(By.XPath(".//th")).ToList();

            for (int i = 0; i < headers.Count; i++)
            {
                string actualHeader = WebObjectExtension.NormalizeText(headers[i].Text);

                if (actualHeader.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return i + 1;
                }
            }

            throw new Exception($"Column '{columnName}' was not found in table '{Name}'.");
        }

        public List<IWebElement> GetRows()
        {
            IWebElement table = this.WaitForElementToBeVisible();

            return table.FindElements(By.XPath(".//tbody//tr"))
                .Where(row => row.Displayed)
                .ToList();
        }

        public IWebElement GetCell(IWebElement row, string columnName)
        {
            int columnIndex = GetColumnIndex(columnName);

            return row.FindElement(By.XPath($"./td[{columnIndex}]"));
        }

        public string GetCellText(IWebElement row, string columnName)
        {
            return WebObjectExtension.NormalizeText(GetCell(row, columnName).Text);
        }
        public bool HasRows()
        {
            return GetRows().Count > 0;
        }

        public List<string> GetColumnTexts(string columnName)
        {
            return GetRows()
                .Select(row => GetCellText(row, columnName))
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .ToList();
        }

        public bool IsImageDisplayedInCell(IWebElement row, string columnName)
        {
            IWebElement cell = GetCell(row, columnName);

            return cell.FindElements(By.TagName("img"))
                .Any(image => image.Displayed);
        }

        public bool AreAllColumnValuesContains(string columnName, string expectedText)
        {
            List<string> values = GetColumnTexts(columnName);

            return values.Count > 0
                && values.All(value =>
                    value.Contains(
                        expectedText,
                        StringComparison.OrdinalIgnoreCase
                    )
                );
        }
        public bool WaitUntilColumnValueDisplayed(string columnName, string expectedText)
        {
            try
            {
                WebObjectExtension.GetWait().Until(driver =>
                {
                    List<string> values = GetColumnTexts(columnName);

                    return values.Any(value =>
                        value.Equals(
                            expectedText,
                            StringComparison.OrdinalIgnoreCase
                        )
                    );
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool IsColumnValueDisplayed(
    string columnName,
    string expectedText,
    int? timeoutInSeconds = null
)
        {
            return WebObjectExtension.WaitUntil(driver =>
            {
                List<string> values = GetColumnTexts(columnName);

                return values.Any(value =>
                    value.Equals(
                        expectedText,
                        StringComparison.OrdinalIgnoreCase
                    )
                );
            }, timeoutInSeconds);
        }
        public bool IsColumnValueDisplayedOrTableEmpty(string columnName, string expectedText)
        {
            bool isValueDisplayed = WebObjectExtension.WaitUntil(driver =>
            {
                try
                {
                    List<string> values = GetColumnTexts(columnName);

                    return values.Any(value =>
                        value.Equals(
                            expectedText,
                            StringComparison.OrdinalIgnoreCase
                        )
                    );
                }
                catch
                {
                    return false;
                }
            }, 5);

            if (isValueDisplayed)
            {
                return true;
            }

            return GetRows().Count == 0;
        }

    }
}