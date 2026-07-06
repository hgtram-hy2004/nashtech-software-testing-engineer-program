using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using HoangTramHuynh.Core.UI;
using HoangTramHuynh.Core.Report;

namespace HoangTramHuynh.Component
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
            ReportLog.Info($"Found {headers.Count} column headers in table '{Name}'. Looking for column '{columnName}'.");

            for (int i = 0; i < headers.Count; i++)
            {
                string actualHeader = WebObjectExtension.NormalizeText(headers[i].Text);

                if (actualHeader.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return i + 1;
                }
                ReportLog.Info($"Checked column header '{actualHeader}' against expected '{columnName}'.");
            }

            throw new Exception($"Column '{columnName}' was not found in table '{Name}'.");
        }

        public List<IWebElement> GetRows()
        {
            IWebElement table = this.WaitForElementToBeVisible();
            ReportLog.Info($"Retrieving rows from table '{Name}'.");
            return table.FindElements(By.XPath(".//tbody//tr"))
                .Where(row => row.Displayed)
                .ToList();
        }

        public IWebElement GetCell(IWebElement row, string columnName)
        {
            int columnIndex = GetColumnIndex(columnName);
            ReportLog.Info($"Retrieving cell from column '{columnName}' (index {columnIndex}) for the given row.");
            return row.FindElement(By.XPath($"./td[{columnIndex}]"));
        }

        public string GetCellText(IWebElement row, string columnName)
        {
            ReportLog.Info($"Retrieving text from cell in column '{columnName}' for row.");
            return WebObjectExtension.NormalizeText(GetCell(row, columnName).Text);
        }
        public List<string> GetColumnTexts(string columnName)
        {
            ReportLog.Info($"Retrieving text values from column '{columnName}' in table '{Name}'.");
            return GetRows()
                .Select(row => GetCellText(row, columnName))
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .ToList();
        }

        public bool AreAllColumnValuesContains(string columnName, string expectedText)
        {
            List<string> values = GetColumnTexts(columnName);
            ReportLog.Info($"Checking if all values in column '{columnName}' contain text '{expectedText}'. Total values found: {values.Count}.");
            return values.Count > 0
                && values.All(value =>
                    value.Contains(expectedText,StringComparison.OrdinalIgnoreCase)
                );
        }
        public bool IsColumnValueDisplayed(string columnName,string expectedText,int? timeoutInSeconds = null)
        {
            ReportLog.Info($"Checking if column '{columnName}' contains value '{expectedText}' with timeout of {timeoutInSeconds ?? 30} seconds.");
            return WebObjectExtension.WaitUntil(driver =>
            {
                List<string> values = GetColumnTexts(columnName);
                return values.Any(value =>value.Equals(expectedText,StringComparison.OrdinalIgnoreCase));
            }, timeoutInSeconds);
        }

    }
}